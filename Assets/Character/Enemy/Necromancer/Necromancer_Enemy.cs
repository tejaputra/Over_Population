using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Enemy enemy;
    private Animator animator;
    [SerializeField] float Health = 0;
    [SerializeField] float Attack = 0;
    [SerializeField] float Movement_Speed = 0;
    [SerializeField] float Point = 0;
    [SerializeField] float Exp = 0;
    [SerializeField] float Range_Attack = 0;
    [SerializeField] float RespawnTime = 3;
    // Start is called before the first frame update

    [SerializeField] GameObject Projectile_Bullet;
    [SerializeField] Transform Bullet_Pivot;

    [SerializeField] GameObject Zombie1;
    [SerializeField] Transform Zombie1_Pivot;
    [SerializeField] GameObject Zombie2;
    [SerializeField] Transform Zombie2_Pivot;
    bool IsAttack = false;
    [SerializeField] int SummonForAtk = 2;
    
    int counter = 0;

    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Start() 
    {
        enemy.setParameter(Health, Attack, Movement_Speed, Point, Exp, RespawnTime);
    }

    // Update is called once per frame
    private int AnimState = Animator.StringToHash("AnimState");
    void FixedUpdate()
    {
        if(enemy.CheckHealth()){
            enemy.DestroyObject();
        } 
        else if(enemy.PlayerDeathCheck())
        {
            if(enemy.CheckAttackInsideMainCamera(Range_Attack) && !IsAttack)
            {
                if(counter > SummonForAtk){
                    enemy.RotationEnemy();
                    animator.SetInteger(AnimState,3);
                    IsAttack = true;
                    counter = 0;
                } 
                else
                {
                    enemy.RotationEnemy();
                    animator.SetInteger(AnimState,2);
                    IsAttack = true;
                }
            } 
            else if(!IsAttack) 
            {
                animator.SetInteger(AnimState,1);
                enemy.MovementEnemy();
            }
        } 
        else 
        {
            animator.SetInteger(AnimState,0);
        }
    } 
    private void ProjectileAttack(){
        if(IsAttack)
        {
            Quaternion dataAngle = enemy.AngleAttack(Bullet_Pivot,0f);
            Vector3 temporaryVector = enemy.AngleDegreeAttack(Bullet_Pivot,0f);

            GameObject Bullet = Instantiate(Projectile_Bullet, Bullet_Pivot.position, dataAngle);
            Bullet.GetComponent<Projectile>().getDirection(temporaryVector);
            Bullet.GetComponent<Projectile>().SetTypeBullet(3);
            Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);
            Bullet.GetComponent<Projectile>().SetSpeed(150);
            Bullet.GetComponent<Projectile>().SetTime(3);
            counter++;
        }
    }

    private void SummonZombie(){
        if(IsAttack)
        {
        GameObject summon = Instantiate(Zombie1, Zombie1_Pivot.position,Zombie1_Pivot.rotation);
        summon.GetComponent<Enemy>().Summoned = true;
        summon = Instantiate(Zombie2, Zombie2_Pivot.position, Zombie2_Pivot.rotation);
        summon.GetComponent<Enemy>().Summoned = true;
        animator.SetInteger("AnimState",1);
        }
    }
    private void AttackFinish(){
        IsAttack = false;
    }
}
