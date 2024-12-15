using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_King_Boss : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    [SerializeField] string BossName = "Orc King";
    [SerializeField] float Health = 0;
    private bool Special = false;
    [SerializeField] float [] PanicHealth;
    private bool Panic = false;
    private int Panic_Time = 0;
    private int Panic_Remainder = 0;
    [SerializeField] private float Healing_in_Percentage = 0;
    [SerializeField] private float Healing_Time = 0;
    private float HealingRemainder = 0; 
    [SerializeField] float Attack = 0;
    [SerializeField] float Movement_Speed = 0;
    [SerializeField] float Point = 0;
    [SerializeField] float Exp = 0;

    [SerializeField] float GiddyTime = 2;
    [SerializeField] Vector2 [] Position_Boss_Move;
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform Pivot_Projectile;
    [SerializeField] List<GameObject> EnemiesReinforcement;
    [SerializeField] List<Vector3> PivotReinforcement;
    private int Boss_Move = 1;

    private int AnimState = 1;

    private bool movement = true;
    private int move_Section = 0;
    private float Scale_X_Boss;
    private bool ChangeAnimation = true;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();

        enemy.setParameterBoss(Health, Attack, Movement_Speed * 20, Point, Exp, BossName);
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
        switch(AnimState)
        {
            case 0:
                animator.Play("Idle");
                break;
            case 1:
                animator.Play("Walk");
                MovementToPosition();
                break;
            case 2:
                animator.Play("Range_Atk", 0);
                break;
            case 3:
                animator.Play("Reinforcement");
                break;
            case 4:
                animator.Play("Healing");
                break;
            case 5:
                animator.Play("Panic_Mode");
                break;
            case 6:
                animator.Play("Run_Attack");
                if(Panic_Time != Panic_Remainder)
                {
                    Panic = true;
                    Panic_Remainder++;
                    aimDirection = ( enemy.playerObject.transform.position - gameObject.transform.position);
                    aimDirection.y += -0.5f;
                    Invoke("AnimationClear", 0.7f);
                    Invoke("SpecialCancel", 0.7f);
                }
                enemy.MovementEnemyTowardPosition(aimDirection);
                break;
            case 7:
                animator.Play("Death");
                break;
        }
        if(Panic_Time < PanicHealth.Length && !Special)
        {
            if(enemy.Health/enemy.MaxHealth * 100 < PanicHealth[Panic_Time])
            {
                Special = true;
                Panic_Time++;
                AnimState = 5;
                Boss_Move = 9;
            }
        }
        HealingRemainder += Time.deltaTime;
        if(HealingRemainder > Healing_Time && !Special){
            if(enemy.Health/enemy.MaxHealth * 100 < 50)
            {
                HealingRemainder = 0;
                Special = true;
                AnimState = 5;
                Boss_Move = 10;
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
        else if (enemy.playerObject == null)
        {
            AnimState = 0;
        }
    }
    void MovementofBoss(){
        if(Boss_Move == 1)
        {
            ChangeAnimationState(1);
            //walk
        } 
        else if (Boss_Move == 2)
        {
            ChangeAnimationState(0);
            RotationtoPlayer();
        } 
        else if (Boss_Move == 3)
        {
            ChangeAnimationState(3);
            //Reinforcement
        } 
        else if (Boss_Move == 4)
        {
            ChangeAnimationState(0);
            RotationtoPlayer();
        } 
        else if (Boss_Move == 5)
        {
            ChangeAnimationState(1);
        //walk
        } 
        else if (Boss_Move == 6)
        {
            ChangeAnimationState(0);
            RotationtoPlayer();
        } 
        else if (Boss_Move == 7)
        {
            //Range_Attack
            ChangeAnimationState(2);
        }
        else if (Boss_Move == 8)
        {
            ChangeAnimationState(0);
            RotationtoPlayer();
        } 
        else if (Boss_Move == 9)
        {
            ChangeAnimationState(6);
        } 
        else if (Boss_Move == 10)
        {
            ChangeAnimationState(4);
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
    void MovementToPosition(){
        if(enemy.playerObject != null)
            movement = enemy.MovementEnemyToPosition(Position_Boss_Move[move_Section]);
        if(!movement && ChangeAnimation == false)
        {
            move_Section++;
            if(move_Section > 3)
                move_Section = 0;
            MovementRotation();
            AnimationClear();
        }
    }
    void RotationtoPlayer(){
        if(enemy.playerObject != null)
        {
            if(enemy.playerObject.transform.position.x + 0.3f > transform.position.x)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
            } 
            else if (enemy.playerObject.transform.position.x - 0.3f < transform.position.x)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
            }
        }
    }
    void MovementRotation(){
        if((move_Section == 3 || move_Section == 0 ))
        {
            transform.localScale = new Vector2(Scale_X_Boss, transform.localScale.y);
        } 
        else if ((move_Section == 1 || move_Section == 2 ))
        {
            transform.localScale = new Vector2(-Scale_X_Boss, transform.localScale.y);
        }
    }
    void ChangeAnimationState(int newState){
        if(AnimState == newState) return;
        AnimState = newState;
    }
    
    private void BossCoinThrower(){
        enemy.RangeAttack(Projectile, Pivot_Projectile, 1, Attack);
    }

    private void ReinforcementMinion(){
        int bykList = PivotReinforcement.Count;
        List<Vector3> pivot_nambah_player = new List<Vector3>();
        for(int i = 0; i < bykList; i++){
            pivot_nambah_player.Add(PivotReinforcement[i]+enemy.playerObject.transform.position);
        }
        List<GameObject> EnemiesAdd = new List<GameObject>();
        EnemiesAdd.AddRange(EnemiesReinforcement);
        enemy.Summoning(EnemiesAdd, pivot_nambah_player);
    }
    private void HealingEating(){
        enemy.HealingPercentage(Healing_in_Percentage);
        
    }
    private void SpecialCancel(){
        Special = false;
    }
    
    private void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Player_Area" && Panic){
            Panic = enemy.DamagePlayerBoss();
            enemy.playerObject.GetComponent<Player_Script>().setGiddy(GiddyTime);
        }
    }
}

