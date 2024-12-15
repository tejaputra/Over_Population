using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich_Boss : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    [SerializeField] string BossName = "Lich";
    [SerializeField] float Health = 0;
    private bool Special = false;
    [SerializeField] float [] PanicHealth;
    private int Panic_Time = 0;
    [SerializeField] float Attack = 0;
    [SerializeField] float Movement_Speed = 0;
    [SerializeField] float Point = 0;
    [SerializeField] float Exp = 0;

    [SerializeField] float NegativeVisionTime = 2;
    [SerializeField] Vector2 [] Position_Boss_Move;
    private int remainderMove = 9;
    [SerializeField] GameObject Projectile;
    [SerializeField] GameObject Projectile2;
    [SerializeField] Transform Pivot_Projectile;
    [SerializeField] List<GameObject> EnemiesReinforcement;
    [SerializeField] List<Vector3> PivotReinforcement;
    
    private int Boss_Move = 1;
    private int AnimState = 5;
    private float Scale_X_Boss;
    private bool ChangeAnimation = true;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();

        enemy.setParameterBoss(Health, Attack, Movement_Speed, Point, Exp, BossName);
        Scale_X_Boss = gameObject.transform.localScale.x;
    }
    void Start()
    {
        GameObject.FindGameObjectWithTag("Game_Information").GetComponent<SpawnController>().turnOnBossBar(enemy.Health, enemy.MaxHealth, enemy.boss, BossName);
        //enemy.turnOnBossBar();
    }

    // Update is called once per frame
    Vector2 position = new Vector3 (5f,5f);
    Vector3 aimDirection = new Vector3(0f,0f,0f);
    void FixedUpdate()
    {
        if (enemy.playerObject == null)
        {
            AnimState = 0;
            return;
        }
        switch(AnimState)
        {
            case 0:
                animator.Play("Idle");
                break;
            case 1:
                animator.Play("Walk");
                break;
            case 2:
                animator.Play("Attack1");
                break;
            case 3:
                animator.Play("Attack2");
                break;
            case 4:
                animator.Play("Reinforcement");
                break;
            case 5:
                animator.Play("Jump_Up");
                break;
            case 6:
                animator.Play("Jump_Down");
                break;
            case 7:
                animator.Play("Death");
                break;
        }
        if(Panic_Time < PanicHealth.Length && !Special)
        {
            if(enemy.Health/enemy.MaxHealth * 100 < PanicHealth[Panic_Time])
            {
                ChangeAnimation = false;
                Special = true;
                Panic_Time++;
                AnimState = 3;
                Boss_Move = 1;
                print("threshold " + Panic_Time);
            }
        }
        if(enemy.Health <= 0){
            AnimState = 7;
            return;
        }
        if(ChangeAnimation)
        {
            ChangeAnimation = false;
            MovementofBoss();
        } 
    }
    void MovementofBoss(){
        if(Boss_Move == 1)
        {
            ChangeAnimationState(5);
        } 
        else if (Boss_Move == 2)
        {
            ChangeAnimationState(6);
        } 
        else if (Boss_Move == 3)
        {
            ChangeAnimationState(2);
            //Attack1
        } 
        else if (Boss_Move == 4)
        {
            ChangeAnimationState(0);
        } 
        else if (Boss_Move == 5)
        {
            ChangeAnimationState(5);
        } 
        else if (Boss_Move == 6)
        {
            ChangeAnimationState(6);
        } 
        else if (Boss_Move == 7)
        {
            ChangeAnimationState(4);
            //reinforcement
        } 
        else if (Boss_Move == 8)
        {
            ChangeAnimationState(0);
        } 
        Boss_Move++;
        if(Boss_Move > 8)
        {
            Boss_Move = 1;
        }
    }
    void AnimationClear(){
        ChangeAnimation = true;
    }
    void Teleport(){
        if(enemy.playerObject != null)
        {
        //Position_Boss_Move
            Vector3 Position = new (0,0,0);
            int random = Random.Range(0,Position_Boss_Move.Length -1);
            while(random == remainderMove)
            {
                random = Random.Range(0,Position_Boss_Move.Length -1);
            }
            remainderMove = random;
            Position += enemy.playerObject.transform.position;
            Position.x += Position_Boss_Move[random].x;
            Position.y += Position_Boss_Move[random].y;
            gameObject.transform.position = Position;
            RotationtoPlayer();
        }
    }
    void RotationtoPlayer(){
        if(enemy.playerObject != null)
        {
            if(enemy.playerObject.transform.position.x + 0.1f > transform.position.x)
            {
                transform.localScale = new Vector2( Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
            } 
            else if (enemy.playerObject.transform.position.x - 0.1f < transform.position.x)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
            }
        }
    }
    void ChangeAnimationState(int newState){
        if(AnimState == newState) return;
        AnimState = newState;
    }

    private void HomingSpell(){
        enemy.RangeAttack(Projectile, Pivot_Projectile, 4, Attack, 150, 9);
    }
    private void NegativeVisionpell(){
        //enemy.RangeAttack(Projectile2, Pivot_Projectile, 5, Attack, 150, 9);
        string test = "Negative_Vision";
        print(test);
        enemy.RangeAttackEffect(Projectile2, Pivot_Projectile, 5, Attack, 150, 9, test, NegativeVisionTime);
        SpecialCancel();
    }

    private void ReinforcementMinion(){
        int bykList = PivotReinforcement.Count;
        List<Vector3> pivot_nambah_player = new List<Vector3>();
        for(int i = 0; i < bykList; i++){
            pivot_nambah_player.Add(PivotReinforcement[i]+enemy.playerObject.transform.position);
        }
        pivot_nambah_player.RemoveAt(remainderMove);
        List<GameObject> EnemiesAdd = new List<GameObject>();
        EnemiesAdd.AddRange(EnemiesReinforcement);
        enemy.Summoning(EnemiesAdd, pivot_nambah_player);
    }
    private void SpecialCancel(){
        Special = false;
    }
}
