using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting_Layer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int SortingOrderBase = 5000;
    [SerializeField] float offset = 0.7f;
    private SpriteRenderer spriteRender;
    private bool runOnce = true;
    private void Awake() {
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
    }
    private void LateUpdate() {
        spriteRender.sortingOrder = (int)(SortingOrderBase - transform.position.y - offset);
        if(runOnce){
            Destroy(this);
        }
    }
}
