using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionEmpentar : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForceAtPosition(GetComponent<MinionMoviment>().Torna_Velocitat(),
                hit.point);
        }
    }
}
