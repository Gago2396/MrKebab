using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAlterarVelocitat : MonoBehaviour
{
    public float f_factor_mult_velocitat = 2f;
    public float f_duracio_efecte = 3f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MinionMoviment>().f_velocitat *= f_factor_mult_velocitat;
        Destroy(gameObject.GetComponent<MinionAlterarVelocitat>(), f_duracio_efecte);
    }

    private void OnDestroy()
    {
        GetComponent<MinionMoviment>().f_velocitat /= f_factor_mult_velocitat;
    }
}
