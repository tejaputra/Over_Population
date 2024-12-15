using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Enemy : MonoBehaviour
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

    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();

    }

    void Start() {
        enemy.setParameter(Health, Attack, Movement_Speed, Point, Exp,RespawnTime);
    }
    private int AnimState = Animator.StringToHash("AnimState");
    void FixedUpdate()
    {
        //animator.SetInteger("AnimState",1);
                //enemy.MovementEnemy();
        if(enemy.CheckHealth()){
            animator.SetInteger(AnimState,3);
        } 
        else if(enemy.PlayerDeathCheck())
        {
            if(enemy.MeleeEnemyCheck() && enemy.CheckAttackInsideMainCamera(Range_Attack))
            {
                animator.SetInteger(AnimState,2);
            } 
            else 
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
}
