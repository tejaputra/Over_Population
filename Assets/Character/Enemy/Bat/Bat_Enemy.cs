using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Enemy enemy;
    private Animator animator;
    [SerializeField] float Health = 0;
    [SerializeField] float Attack = 0;
    [SerializeField] float Movement_Speed = 0;
    [SerializeField] float Point = 0;
    [SerializeField] float Exp = 0;
    [SerializeField] float RespawnTime = 3;

    [SerializeField] float Range_Attack = 0;
    // Start is called before the first frame update

    [SerializeField] GameObject Projectile_Bullet;
    [SerializeField] Transform Bullet_Pivot;
    bool IsAttack = false;

    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();

        enemy.setParameter(Health, Attack, Movement_Speed, Point, Exp, RespawnTime);
    }

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
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                enemy.RotationEnemy();
                animator.SetInteger(AnimState,2);
                IsAttack = true;
            } 
            else if(!IsAttack) 
            {
                animator.SetInteger(AnimState,1);
                enemy.MovementEnemy();
            } 
            else if(animator.GetInteger(AnimState) == 0)
            {
                IsAttack = false;
            }
        } 
        else 
        {
            animator.SetInteger(AnimState,0);
        }
    } 

    public void TeleportNoJutsu(){
        if(enemy.playerObject != null){
            Vector3 PlayerPosition = enemy.playerObject.transform.position;
            float offset = 2f;
            Vector2 position_Teleport;
            position_Teleport.x = Random.Range(-4f,4f);
            position_Teleport.y = Random.Range(-2.5f,2.5f);
            if(position_Teleport.x >= 0)
            {
                position_Teleport.x += PlayerPosition.x + offset;
            } else
            {
                position_Teleport.x += PlayerPosition.x - offset;
            }

            if(position_Teleport.y >= 0)
            {
                position_Teleport.y += PlayerPosition.y + offset;
            } else
            {
                position_Teleport.y += PlayerPosition.y - offset;
            }
            transform.position = position_Teleport;
        }
    }
    Quaternion dataAngle;
    Vector3 temporaryVector;
    private void TripleProjectileAttack(){
        dataAngle = enemy.AngleAttack(Bullet_Pivot,0f);
        temporaryVector = enemy.AngleDegreeAttack(Bullet_Pivot,0f);
        Invoke("delayAttack", 0.2f);

        GameObject Bullet = Instantiate(Projectile_Bullet, Bullet_Pivot.position, enemy.AngleAttack(Bullet_Pivot,-30f));
        Bullet.GetComponent<Projectile>().getDirection(enemy.AngleDegreeAttack(Bullet_Pivot,-30f));
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);

        Bullet = Instantiate(Projectile_Bullet, Bullet_Pivot.position, enemy.AngleAttack(Bullet_Pivot, 30f));
        Bullet.GetComponent<Projectile>().getDirection(enemy.AngleDegreeAttack(Bullet_Pivot,30f));
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);
    }
    private void delayAttack(){
        GameObject Bullet = Instantiate(Projectile_Bullet, Bullet_Pivot.position, dataAngle);
        Bullet.GetComponent<Projectile>().getDirection(temporaryVector);
        Bullet.GetComponent<Projectile>().SetTypeBullet(1);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Attack);
    }
    private void AttackClear(){
        IsAttack = false;
    }
}
