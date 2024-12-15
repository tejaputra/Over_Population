using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_King_Boss : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    [SerializeField] string BossName = "Slime King";
    [SerializeField] float Health = 0;
    private bool Special = false;
    [SerializeField] float [] PanicHealth;
    private int Panic_Time = 0;
    [SerializeField] float Attack = 0;
    [SerializeField] float Movement_Speed = 0;
    [SerializeField] float Point = 0;
    [SerializeField] float Exp = 0;

    [SerializeField] float SlowTime = 2;
    [SerializeField] float SlowSpeed = 0.6f;
    [SerializeField] GameObject Projectile;
     [SerializeField] GameObject Projectile2;
    [SerializeField] Transform Pivot_Projectile;
    [SerializeField] List<GameObject> EnemiesReinforcement1;
    [SerializeField] List<GameObject> EnemiesReinforcement2;
    [SerializeField] List<Vector3> PivotReinforcement;
    private int Boss_Move = 1;
    private int AnimState = 1;
    private float Scale_X_Boss;
    private bool ChangeAnimation = false;
    private bool rage = false;
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
    bool jump = true;
    bool jumpTimer = false;

    float timerJump = 3.2f;
    float timerJumpRemainder = 0f;
    Vector2 jumpPosition = new Vector2(0,0);
    [SerializeField] GameObject BossShadow;
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
                enemy.MovementEnemy();
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
                animator.Play("Reinforcement2");
                break;
            case 6:
                animator.Play("Jump_Up");
                if(jump)
                {
                    jumpPosition = gameObject.transform.position;
                    jumpPosition.y += 20;
                    jump = false;
                    print("huhuhuh");
                }
                print( "Jump Position:  "+ jumpPosition);
                enemy.MovementEnemyToPosition(jumpPosition, 1f);
                break;
            case 7:
                animator.Play("Jump_Down");
                if(GameObject.FindWithTag("Shadow") != null)
                    enemy.MovementEnemyToPosition(GameObject.FindWithTag("Shadow").transform.position, 1f);
                break;
            case 8:
                animator.Play("Death");
                break;
        }
        print(AnimState + " " + Boss_Move + " Special : " + Special);

        if(Panic_Time < PanicHealth.Length && !Special)
        {
            if(enemy.Health/enemy.MaxHealth * 100 < PanicHealth[Panic_Time])
            {
                Special = true;
                Panic_Time++;
                ChangeAnimation = false;
                AnimState = 6;
                Boss_Move = 1;
            }
        }
        if(enemy.Health <= 0){
            AnimState = 8;
            return;
        }
        if(enemy.Health/enemy.MaxHealth * 100 <= 50)
            rage = true;
        else
            rage = false;
        print("rage " + rage);
        if(ChangeAnimation)
        {
            ChangeAnimation = false;
            if(rage)
                MovementofBoss2();
            else
                MovementofBoss();
        } 
        if(jumpTimer)
        {
            timerJumpRemainder += Time.deltaTime;
            if(timerJumpRemainder > timerJump){
                timerJumpRemainder = 0;
                jumpTimer = false;
                Vector3 bossPosition = gameObject.transform.position;
                bossPosition.x = GameObject.FindWithTag("Shadow").transform.position.x;
                gameObject.transform.position = bossPosition;
                AnimState = 7;
                
            }
        }
    }
    void ShadowOn(){
        BossShadow.GetComponent<Slime_King_Shadow>().Timer = 3f;
        Vector3 shadowPosition = new Vector3(0,0.2f);
        shadowPosition += gameObject.transform.position;
        BossShadow.transform.position = shadowPosition;

        GameObject Shadow = Instantiate(BossShadow,shadowPosition, BossShadow.transform.rotation);
        
        jumpTimer = true;
    }
    void ShadowOff(){
        AnimState = 1;
        Boss_Move = 1;
        jump = true;
        Destroy(GameObject.FindWithTag("Shadow"));
    }
    void MovementofBoss(){
        if(Boss_Move == 1)
        {
            ChangeAnimationState(1);
        } 
        else if (Boss_Move == 2)
        {
            ChangeAnimationState(2);
        } 
        else if (Boss_Move == 3)
        {
            ChangeAnimationState(1);
            //Attack1
        } 
        else if (Boss_Move == 4)
        {
            ChangeAnimationState(2);
        } 
        Boss_Move++;
        if(Boss_Move > 4)
        {
            Boss_Move = 1;
        }
    }

    void MovementofBoss2(){
        if(Boss_Move == 1)
        {
            ChangeAnimationState(1);
        } 
        else if (Boss_Move == 2)
        {
            ChangeAnimationState(3);
        } 
        else if (Boss_Move == 3)
        {
            ChangeAnimationState(1);
            //Attack1
        } 
        else if (Boss_Move == 4)
        {
            ChangeAnimationState(5);
        } 
        Boss_Move++;
        if(Boss_Move > 4)
        {
            Boss_Move = 1;
        }
    }
    void AnimationClear(){
        ChangeAnimation = true;
    }
    //## ada beberapa tick yg bikin error dengan waktu
    void ChangeAnimationState(int newState){
        if(AnimState == newState) return;
        AnimState = newState;
    }

    private void SlimeBall(){
        enemy.RangeAttack(Projectile, Pivot_Projectile, 1, Attack, 250, 5);

        GameObject Bullet = Instantiate(Projectile, Pivot_Projectile.position, enemy.AngleAttack(Pivot_Projectile,-25f));
        Bullet.GetComponent<Projectile>().getDirection(enemy.AngleDegreeAttack(Pivot_Projectile,-25f));
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);

        Bullet = Instantiate(Projectile, Pivot_Projectile.position, enemy.AngleAttack(Pivot_Projectile,25f));
        Bullet.GetComponent<Projectile>().getDirection(enemy.AngleDegreeAttack(Pivot_Projectile,25f));
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);
        //enemy.AngleAttack(Bullet_Pivot,-30f)
        
    }

    private void SlimeBallRage(){
        enemy.RangeAttack(Projectile2, Pivot_Projectile, 1, Attack, 250, 5);
        GameObject Bullet = Instantiate(Projectile2, Pivot_Projectile.position, enemy.AngleAttack(Pivot_Projectile,-25f));
        Bullet.GetComponent<Projectile>().getDirection(enemy.AngleDegreeAttack(Pivot_Projectile,-25f));
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);

        Bullet = Instantiate(Projectile2, Pivot_Projectile.position, enemy.AngleAttack(Pivot_Projectile,25f));
        Bullet.GetComponent<Projectile>().getDirection(enemy.AngleDegreeAttack(Pivot_Projectile,25f));
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);

        Bullet = Instantiate(Projectile2, Pivot_Projectile.position, enemy.AngleAttack(Pivot_Projectile,50f));
        Bullet.GetComponent<Projectile>().getDirection(enemy.AngleDegreeAttack(Pivot_Projectile,50f));
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);
        Bullet = Instantiate(Projectile2, Pivot_Projectile.position, enemy.AngleAttack(Pivot_Projectile,-50f));
        Bullet.GetComponent<Projectile>().getDirection(enemy.AngleDegreeAttack(Pivot_Projectile,-50f));
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);
    }

    private void ReinforcementMinion(){
        int bykList = PivotReinforcement.Count;
        List<Vector3> pivot_nambah_player = new List<Vector3>();
        for(int i = 0; i < bykList; i++){
            pivot_nambah_player.Add(PivotReinforcement[i]+gameObject.transform.position);
        }
        List<GameObject> EnemiesAdd = new List<GameObject>();
        EnemiesAdd.AddRange(EnemiesReinforcement1);
        enemy.Summoning(EnemiesAdd, pivot_nambah_player);
    }

    private void ReinforcementMinion2(){
        int bykList = PivotReinforcement.Count;
        List<Vector3> pivot_nambah_player = new List<Vector3>();
        for(int i = 0; i < bykList; i++){
            pivot_nambah_player.Add(PivotReinforcement[i]+gameObject.transform.position);
        }
        List<GameObject> EnemiesAdd = new List<GameObject>();
        EnemiesAdd.AddRange(EnemiesReinforcement2);
        enemy.Summoning(EnemiesAdd, pivot_nambah_player);
    }
    private void SpecialCancel(){
        Special = false;
    }
    private void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Player_Area" && jump == true){
            enemy.playerObject.GetComponent<Player_Script>().setSlow(SlowTime, SlowSpeed);
        }
    }
    private void OnTriggerStay2D(Collider2D target) {
        if(target.tag == "Player_Area" && jump == true){
            enemy.playerObject.GetComponent<Player_Script>().setSlow(SlowTime, SlowSpeed);
        }
    }
}
