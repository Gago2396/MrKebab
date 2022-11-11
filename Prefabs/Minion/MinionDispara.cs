using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDispara : MonoBehaviour
{
    public GameObject go_projectil;
    public Transform t_punt_dispar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (go_projectil == null)
            return;


        if (Input.GetMouseButtonDown(0))
            Instantiate(go_projectil, t_punt_dispar.position, t_punt_dispar.rotation);
        
    }
}
