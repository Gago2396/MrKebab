using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GestionIngredientes: MonoBehaviour
{
    public Color color;
    public Collider Collider;
    public List<string> ingredientes;
    IAComportamiento IA;
    public GameManager manager;
    bool pressed;
    
    public void Start()
    {
        ingredientes = new List<string>();
    }
    public void Update()
    {
        if (manager.IAexiste)
        {
            IA = GameObject.FindGameObjectWithTag("IA").GetComponent<IAComportamiento>();
        }
        ingredientes.Sort();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("ingrediente") || other.gameObject.CompareTag("IA"))
        {
            Collider = other;
            Debug.Log(other.gameObject.name);
            color = other.gameObject.GetComponent<Renderer>().material.color;
            other.gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b) * 3;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ingrediente"))
        {
            if (((Input.GetKey(KeyCode.E)) || manager.b_usar.pressed) && !pressed)
            {
                pressed = true;
                ingredientes.Add(other.gameObject.name);
            }
            if (!Input.GetKey(KeyCode.E) && !manager.b_usar.pressed)
            {
                pressed = false;
            }
        }
        if (other.gameObject.CompareTag("IA"))
        {
            if (((Input.GetKey(KeyCode.E)) || manager.b_usar.pressed) && !pressed)
            {
                pressed = true;
                
                if (ingredientes.SequenceEqual(IA.ingredientes))
                {
                    IA.pedidoBien = true;
                }
                else
                {
                    IA.pedidoMal = true;
                }
                ingredientes.Clear();
            }
            if (!Input.GetKey(KeyCode.E))
            {
                pressed = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Renderer>().material.color = color;
    }


}
