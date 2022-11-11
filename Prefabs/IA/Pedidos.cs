using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedidos : MonoBehaviour
{
    string[] ingredientes1 = { "Carne de Ternera", "Carne de Pollo", "Lechuga", "Salsa Blanca" };
    //string[] ingredientes2 = { "Lechuga", "Salsa Blanca" };
    string[] ingredientes2 = { "Tomate", "Salsa Barbacoa" };
    string[] ingredientes3 = { "Salsa Picante", "Cebolla" };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string pedido(int numero)
    {
        string pedido = string.Empty;

        switch(numero){
            case 1:
                Debug.Log(Random.Range(0, 4));
                pedido += ingredientes1[Random.Range(0, 4)] + "\n";
                pedido += ingredientes1[Random.Range(0, 4)] + "\n";
                break;
            case 2:
                pedido += ingredientes1[Random.Range(0, 4)] + "\n";
                pedido += ingredientes1[Random.Range(0, 4)] + "\n";
                pedido += ingredientes2[Random.Range(0, 2)] + "\n";
                break;
            case 3:
                pedido += ingredientes1[Random.Range(0, 4)] + "\n";
                pedido += ingredientes1[Random.Range(0, 4)] + "\n";
                pedido += ingredientes2[Random.Range(0, 2)] + "\n";
                pedido += ingredientes3[Random.Range(0, 2)] + "\n";
                break;
        }

        return pedido;
    }
}
