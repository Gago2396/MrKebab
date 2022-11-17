using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionIngredientes: MonoBehaviour
{
    public Color color;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ingrediente")
        {
            color = other.gameObject.GetComponent<Renderer>().material.color;
            other.gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
        other.gameObject.GetComponent<Renderer>().material.color = color;
    }
}
