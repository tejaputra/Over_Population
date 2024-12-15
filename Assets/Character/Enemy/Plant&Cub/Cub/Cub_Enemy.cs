using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cub_Enemy : MonoBehaviour
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
    private GameObject MotherPlant;
    private bool curse = false;
    
    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();

        enemy.setParameter(Health, Attack, Movement_Speed, Point, Exp, RespawnTime);
    }

    private int AnimState = Animator.StringToHash("AnimState");
    // Update is called once per frame
     void FixedUpdate()
    {
        if(MotherPlant == null)
        {
           //## tidak ada animasi mati.
           enemy.DestroyObject();
        } 
        else if(!curse)
        {
            enemy.MovementEnemy();
        }
        else 
        {
            if(enemy.playerObject != null)
            {
            Vector3 positionPlayer = enemy.playerObject.transform.position;
            positionPlayer.y += -0.4f;
            gameObject.transform.position = positionPlayer;
            //curse = true;
            }
        }
        if(curse && enemy.PlayerDeathCheck() && enemy.CheckAttackInsideMainCamera(Range_Attack))//##attack melee untuk wormy masih ngaco
        {
            animator.SetInteger(AnimState,2);
            //Debug.Log("mukul pemain");
        } 
        else
        {
            animator.SetInteger(AnimState,0);
        }
    }

    public void setMotherPlant(GameObject mother){
        MotherPlant = mother;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player_Area")
        curse = true;
    }

}
