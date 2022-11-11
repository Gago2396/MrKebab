using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IAComportamiento : MonoBehaviour
{

    public Transform spawn;
    public Transform parada;
    public Transform fin;
    public GameObject ia;
    public GameObject cuerpo_ia;
    public TMP_Text muestraPedido;
    public GameManager manager;
    public RelojIA reloj;
    public Pedidos pedidos;
    public float tiempoParcial;
    public float velocidad;
    public int puntos;
    public bool esta_barra = false;
    public bool esta_fin = false;
    public bool pedidoHecho;
    public bool finTiempo;

    // Start is called before the first frame update
    void Start()
    {
        muestraPedido = gameObject.GetComponentInChildren<TMP_Text>();
        pedidoHecho = false;
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<GameManager>();
        manager.IAexiste = true;
        cuerpo_ia.transform.position = spawn.position;
        switch (manager.level)
        {
            case 1:
                muestraPedido.text = pedidos.pedido(1);
                break;
            case 2:
                muestraPedido.text = pedidos.pedido(2);
                break;
            case 3:
                muestraPedido.text = pedidos.pedido(3);
                break;
            default:
                muestraPedido.text = "Error!!";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.reloj.final)
        {
            if (cuerpo_ia.transform.position == parada.position || pedidoHecho)
            {
                if (!esta_barra && !pedidoHecho)
                {
                    cuerpo_ia.transform.Rotate(new Vector3(0, 1, 0), 90);
                    esta_barra = true;
                    reloj.inicio(tiempoParcial);
                }
                if (esta_barra && !pedidoHecho)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        puntos = 100;
                        pedidoHecho = true;
                        EnviaPuntos();
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        puntos = -100;
                        pedidoHecho = true;
                        EnviaPuntos();
                    }
                }
                if (esta_barra && pedidoHecho)
                {
                    cuerpo_ia.transform.Rotate(new Vector3(0, 1, 0), 180);
                    esta_barra = false;
                }
                if (!esta_barra && pedidoHecho)
                {
                    if (cuerpo_ia.transform.position == fin.position)
                    {
                        if (!esta_fin)
                        {
                            esta_fin = true;
                            cuerpo_ia.transform.Rotate(new Vector3(0, 1, 0), 180);
                        }
                        else
                        {
                            Destroy(ia);
                            manager.IAexiste = false;
                        }

                    }
                    else
                    {
                        cuerpo_ia.transform.position = Vector3.MoveTowards(cuerpo_ia.transform.position, fin.position, velocidad * Time.deltaTime);
                    }

                }
                if (reloj.final && !pedidoHecho)
                {
                    pedidoHecho = true;
                    puntos = -100;
                    EnviaPuntos();
                }
                
                Debug.Log(reloj.tiempoRestante);
            }
            else
            {
                cuerpo_ia.transform.position = Vector3.MoveTowards(cuerpo_ia.transform.position, parada.position, velocidad * Time.deltaTime);
            }


        }
        else
        {
            if (!esta_fin)
            {
                cuerpo_ia.transform.position = Vector3.MoveTowards(cuerpo_ia.transform.position, fin.position, velocidad * Time.deltaTime);
                if (cuerpo_ia.transform.position == fin.position)
                {
                    esta_fin = true;
                }
            }
            else
            {
                Destroy(ia);
                manager.IAexiste = false;
            }

        }
    }

    private void EnviaPuntos()
    {
        Color color;
        if (puntos < 0)
        {
            color = Color.red;
        }
        else
        {
            color = Color.green;
        }
        manager.txt_animacion.color = color;
        manager.txt_animacion.text = puntos.ToString();
        manager.puntos += puntos;
    }
}
