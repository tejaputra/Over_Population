using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_Trigger : MonoBehaviour
{
    public event System.Action<Collider2D> EnteredTrigger;
    public event System.Action<Collider2D> ExitTrigger;
    void OnTriggerEnter2D(Collider2D collider) {
        EnteredTrigger?.Invoke(collider);
    }

    void OnTriggerExit2D(Collider2D collider) {
        ExitTrigger?.Invoke(collider);
    }
}
