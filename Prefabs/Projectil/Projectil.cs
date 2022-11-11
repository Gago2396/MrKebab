using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    public GameObject go_explosio;

    public float f_velocitat = 10f;
    public float f_vida = 5f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.TransformVector(Vector3.forward * f_velocitat);

        Destroy(gameObject, f_vida);
    }

    public float f_radius = 5.0F;
    public float f_power = 10.0F;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 v3_explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(v3_explosionPos, f_radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(f_power, v3_explosionPos, f_radius, 3.0F);
            else
            {
                MinionMoviment mv  = hit.GetComponent<MinionMoviment>();

                if (mv != null)
                    mv.AddExplosionForce(f_power, v3_explosionPos, f_radius, 3.0F);
            }
        }

        Destroy(Instantiate(go_explosio, transform.position, Quaternion.identity),3f);
        Destroy(gameObject);
    }


}
