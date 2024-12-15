using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkSwordHolder : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    public void SetDamage(float AtkSword){
        damage = AtkSword;
    }
    public float GetDamage(){
        return damage;
    }
}
