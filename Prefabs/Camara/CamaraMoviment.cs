using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMoviment : MonoBehaviour
{
    public GameObject go_personatgeASeguir;
    Vector3 v3_velocitat_smoothDamp = Vector3.zero;
    Vector3 v3_orientar_smoothDamp = Vector3.zero;
    public float f_temps_SmoothDamp = 0.3f;
    public float f_distancia_mirar = 1000f;
    public Vector3 v3_distancia_rspecte_persontage = Vector3.zero;
    public bool b_posicio = true;
    public bool b_mirar_a = true;


    // Update is called once per frame
    void Update()
    {
        if (go_personatgeASeguir == null)
            return;
        if (b_posicio)
            transform.position = Vector3.SmoothDamp(transform.position,
                go_personatgeASeguir.transform.TransformPoint(v3_distancia_rspecte_persontage),
                ref v3_velocitat_smoothDamp,
                f_temps_SmoothDamp);

        if (b_mirar_a)
            transform.LookAt(Vector3.SmoothDamp(
                transform.TransformPoint(Vector3.forward * f_distancia_mirar),
                go_personatgeASeguir.transform.TransformPoint(Vector3.forward * f_distancia_mirar),
                ref v3_orientar_smoothDamp,
                f_temps_SmoothDamp));
    }
}
