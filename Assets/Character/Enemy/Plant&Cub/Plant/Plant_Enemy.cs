using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Enemy : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;

    [SerializeField] float Health = 0;
    [SerializeField] float Attack = 0;
    [SerializeField] float Movement_Speed = 0;
    [SerializeField] float Point = 0;
    [SerializeField] float Exp = 0;
    [SerializeField] float Range_Attack = 0;
    [SerializeField] float RespawnTime = 3;
    [SerializeField] GameObject cub;
    [SerializeField] Transform Pivot;
    private bool Created_One_Cub = true;
    private bool summon = true;
    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();

        Created_One_Cub = true;
        summon = true;

        enemy.setParameter(Health, Attack, Movement_Speed, Point, Exp, RespawnTime);
    }
    
    int animStatei = 0;
    private int AnimState = Animator.StringToHash("AnimState");
    // Update is called once per frame
     void FixedUpdate()
    {
        print("created one" + Created_One_Cub + " summon " + summon);
        if(enemy.CheckHealth())
        {
           //## tidak ada animasi mati.
           enemy.DestroyObject();
        } 
        if(enemy.PlayerDeathCheck() && Created_One_Cub && enemy.CheckAttackInsideMainCamera(Range_Attack))//##attack melee untuk wormy masih ngaco
        {
            animator.SetInteger(AnimState,2);
            animStatei = 2;
            //Debug.Log("mukul pemain");
            Created_One_Cub = false;
        } 
        else if (animStatei != 2)
        {
            animator.SetInteger(AnimState,0);
            enemy.MovementEnemy();
        }
    }

    private void CreateCub(){
        if(summon)
        {
            GameObject cub_Plant = Instantiate(cub, Pivot.transform.position, Pivot.transform.rotation);
            cub_Plant.GetComponent<Cub_Enemy>().setMotherPlant(gameObject);
            cub_Plant.GetComponent<Enemy>().Summoned = true;
            summon = false;
            animStatei = 0;
        }
    }
}
