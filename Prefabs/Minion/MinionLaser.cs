using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLaser : MonoBehaviour
{
    public GameObject go_explosio;

    public Transform t_punt_dispar;
    public float f_radi_accio_laser = 100f;
    public float f_radius = 5.0F;
    public float f_power = 10.0F;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rch;
            if (Physics.Raycast(t_punt_dispar.position, t_punt_dispar.TransformVector(Vector3.forward),
               out rch, f_radi_accio_laser))
            {
                Vector3 v3_explosionPos = rch.point;
                Collider[] colliders = Physics.OverlapSphere(v3_explosionPos, f_radius);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                        rb.AddExplosionForce(f_power, v3_explosionPos, f_radius, 3.0F);
                    else
                    {
                        MinionMoviment mv = hit.GetComponent<MinionMoviment>();

                        if (mv != null)
                            mv.AddExplosionForce(f_power, v3_explosionPos, f_radius, 3.0F);
                    }
                }
                DibuixaLaser(t_punt_dispar.position, rch.point);
                Destroy(Instantiate(go_explosio, rch.point, Quaternion.identity), 3f);
            }
            else
                DibuixaLaser(t_punt_dispar.position, t_punt_dispar.TransformPoint(Vector3.forward * f_radi_accio_laser));
        }
    }


    void DibuixaLaser(Vector3 v3_ini, Vector3 v3_final)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = v3_ini;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startColor = Color.red;
        lr.endColor = Color.blue;
        lr.startWidth = 0.1f;
        lr.endWidth =  0.1f;
        lr.SetPosition(0, v3_ini);
        lr.SetPosition(1, v3_final);
        GameObject.Destroy(myLine, 1f);
    }
}
