using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonComportamiento : MonoBehaviour
{
    public Button boton;
    public bool pressed;

    private void Start()
    {
        boton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        pressed = true;
        Invoke("Negar", 0.1f);
    }

    private void Negar()
    {
        pressed = false;
    }

}
