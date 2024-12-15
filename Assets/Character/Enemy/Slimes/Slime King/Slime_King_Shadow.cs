using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_King_Shadow : MonoBehaviour
{
    // Update is called once per frame 
    internal float Timer = 3;
    float TimeRemainder = 0;
    void Start() {
    }
    void FixedUpdate()
    {
        TimeRemainder += Time.deltaTime;
        if(GameObject.FindWithTag("Player") != null && TimeRemainder < Timer)
        {
            Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
            playerPosition.y -= 0.8f;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerPosition, 0.1f);
        }
    }
}
