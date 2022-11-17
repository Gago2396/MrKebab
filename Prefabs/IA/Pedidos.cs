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

    public string pedido(int numero, out List<string> ingredientes)
    {
        string pedido = string.Empty;
        ingredientes = new List<string>();
        switch(numero){
            case 1:
                Debug.Log(Random.Range(0, 4));
                ingredientes.Add(ingredientes1[Random.Range(0, 4)]);
                ingredientes.Add(ingredientes1[Random.Range(0, 4)]);
                break;
            case 2:
                ingredientes.Add(ingredientes1[Random.Range(0, 4)]);
                ingredientes.Add(ingredientes1[Random.Range(0, 4)]);
                ingredientes.Add(ingredientes2[Random.Range(0, 2)]);
                break;
            case 3:
                ingredientes.Add(ingredientes1[Random.Range(0, 4)]);
                ingredientes.Add(ingredientes1[Random.Range(0, 4)]);
                ingredientes.Add(ingredientes2[Random.Range(0, 2)]);
                ingredientes.Add(ingredientes3[Random.Range(0, 2)]);
                break;
        }
        ingredientes.Sort();
        foreach (string item in ingredientes)
        {
            pedido += item + "\n";
        }
        return pedido;
    }
}
