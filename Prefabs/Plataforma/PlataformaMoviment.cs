using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMoviment : MonoBehaviour
{
    public Transform t_plfrm;
    public Transform t_punt1;
    public Transform t_punt2;

    public float f_velocitat = 5f;

    Vector3 v3_objectiu = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        v3_objectiu = t_punt1.position;
    }

    // Update is called once per frame
    void Update()
    {
        t_plfrm.position = Vector3.MoveTowards(t_plfrm.position, v3_objectiu, f_velocitat * Time.deltaTime);

        if(t_plfrm.position == t_punt1.position)
            v3_objectiu = t_punt2.position;
        if (t_plfrm.position == t_punt2.position)
            v3_objectiu = t_punt1.position;
    }
}
