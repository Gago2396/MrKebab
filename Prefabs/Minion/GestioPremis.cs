using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestioPremis : MonoBehaviour
{

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.name == "PremiVelocitat")
        {
            gameObject.AddComponent<MinionAlterarVelocitat>();
            Destroy(hit.gameObject);
        }
    }


}
