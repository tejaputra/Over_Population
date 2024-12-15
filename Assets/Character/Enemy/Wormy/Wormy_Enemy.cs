using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormy_Enemy : MonoBehaviour
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
    // Start is called before the first frame update
    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();

        enemy.setParameter(Health, Attack, Movement_Speed, Point, Exp ,RespawnTime);
    }

    // Update is called once per frame
    AnimatorStateInfo animStateInfo;
    private float NTime = 10;
    private int AnimState = Animator.StringToHash("AnimState");
    void FixedUpdate()
    {
        if(enemy.CheckHealth())
        {
           //## tidak ada animasi mati.
           enemy.DestroyObject();
        } 
        else if(enemy.PlayerDeathCheck())
        {
            if(enemy.CheckAttackInsideMainCamera(Range_Attack))
            {
                animator.SetInteger(AnimState,2);
                //Debug.Log("mukul pemain");
                animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                NTime = animStateInfo.normalizedTime;
            } 
            else if(NTime > 1.0f)
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

    private void Teleport(){
        if(enemy.playerObject != null)
        {
            Vector3 positionPlayer = enemy.playerObject.transform.position;
            if(positionPlayer.x > transform.position.x)
            {
            //gameObject.GetComponent<SpriteRenderer>().flipX = false;
                transform.localScale = new Vector2( enemy.Scale_X, transform.localScale.y);
            } 
            else if (positionPlayer.x < transform.position.x)
            {
                //gameObject.GetComponent<SpriteRenderer>().flipX = true;
                transform.localScale = new Vector2( -enemy.Scale_X, transform.localScale.y);
            }

            if(gameObject.transform.position.x > enemy.playerObject.transform.position.x)
            {
                positionPlayer.x += 0.7f;
                positionPlayer.y -= 0f;
            }
            else
            {
                positionPlayer.x -= 0.7f;
                positionPlayer.y -= 0f;
            }
            gameObject.GetComponent<Rigidbody2D>().MovePosition(positionPlayer);
        }
    }
}
