using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailerHelper : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pivot;
    [SerializeField] float speedMovement = 250f;

    [SerializeField] bool Trasparent = true;
    [SerializeField] bool Reverse_Transparent = false;
    [SerializeField] float speedTransparent = 0.008f;

    [SerializeField] float timeDelay = 0f;
    float timeRemainder = 0;
    
    Vector2 position;
    // Update is called once per frame
    void Awake()
    {
    }

    void FixedUpdate()
    {
        timeRemainder += Time.deltaTime;
        if(pivot != null && timeDelay <= timeRemainder)
        {
            transform.position = Vector3.MoveTowards(transform.position, pivot.transform.position, speedMovement * Time.deltaTime);

            if(Trasparent)
            {
                Color dataTransarent = gameObject.GetComponent<Image>().color;
                if(Reverse_Transparent)
                    dataTransarent.a += speedTransparent;
                else
                    dataTransarent.a -= speedTransparent;
                gameObject.GetComponent<Image>().color = dataTransarent;
            }
        }
    }
}
