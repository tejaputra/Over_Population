using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwordSpriteOff(){
        //Animation_Attack = true;
    }
    private void SwordCancel(){
        gameObject.GetComponent<Animator>().SetBool("Sword_Atk",false);
        //Animation_Attack = false;
    }
    public void SwordColliderOn(){
        GameObject.FindWithTag("Player_Melee").GetComponent<CircleCollider2D>().enabled = true;
    }
    public void SwordColliderOff(){
        GameObject.FindWithTag("Player_Melee").GetComponent<CircleCollider2D>().enabled = false;
    }

}
