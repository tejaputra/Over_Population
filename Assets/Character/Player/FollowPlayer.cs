using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject PlayerObject;
    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerObject != null){
            Vector3 currentPostion = PlayerObject.transform.position;
            if(gameObject.name == "Main_Camera")
                currentPostion.z = -7.5f;
            else 
                currentPostion.z = 1f;
            
            transform.position = currentPostion;
        } else if(gameObject.name == "Weapon")
            Destroy(gameObject);
    }
}
