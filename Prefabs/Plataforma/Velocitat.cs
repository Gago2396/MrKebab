using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocitat : MonoBehaviour
{
    public Vector3 v3_g_velocitat = Vector3.zero;

    Vector3 v3_posicio_anterior = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        v3_posicio_anterior = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        v3_g_velocitat = (transform.position - v3_posicio_anterior) / Time.deltaTime;
        v3_posicio_anterior = transform.position;
    }
}
