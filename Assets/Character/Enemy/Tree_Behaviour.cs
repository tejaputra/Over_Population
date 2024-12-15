using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Behaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerStay2D(Collider2D target) {
        if(target.tag == "Player_Foot" || target.tag == "Player_Area")
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            Color currentColor = sprite.color;
            currentColor.a = 0.5f;
            sprite.color = currentColor;
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag == "Player_Foot" || target.tag == "Player_Area")
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            Color currentColor = sprite.color;
            currentColor.a = 1f;
            sprite.color = currentColor;
            //print("out");
        }
    }
}
