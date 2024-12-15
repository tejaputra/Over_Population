using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
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

    //[SerializeField] float Range_Attack = 0;
    // Start is called before the first frame update

    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();

        enemy.setParameter(Health, Attack, Movement_Speed, Point, Exp, RespawnTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //animator.SetInteger("AnimState",1);
                //enemy.MovementEnemy();
        if(enemy.CheckHealth()){
            enemy.DestroyObject();
        } 
        else if(enemy.PlayerDeathCheck())
        {
            
        } 
        else 
        {
            
        }
    } 
}
