using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public BotonComportamiento b_bien;
    public BotonComportamiento b_mal;
    public BotonComportamiento b_menu;
    public BotonComportamiento b_atras;
    public BotonComportamiento b_salir;
    public MinionMoviment minion;
    public Reloj reloj;
    public GameObject menu;
    public GameObject controles;
    public GameObject IA;
    GameObject IAcopia;
    IAComportamiento IAcomp;
    public int puntos;
    public TMP_Text txt_puntos;
    public TMP_Text txt_level;
    public TMP_Text txt_animacion;
    public bool animEnabled;
    public float vel_animacion;
    public Transform animacion_inicio;
    public Transform animacion_final;
    public Transform spawnIA;
    public Slider s_sensi;
    public TMP_Text txt_sensi;
    public Slider s_jump;
    public TMP_Text txt_jump;
    public Slider s_time;
    public TMP_Text txt_time;
    public float tiempo;
    public bool es_tactil;
    public bool IAexiste;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        puntos = 0;
        tiempo = 30;
        IAexiste = false;
        reloj.inicio(tiempo);
        reloj.final = false;
        if (Input.touchSupported)
        {
            es_tactil = true;
        }
        else
        {
            es_tactil = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animEnabled = txt_animacion.enabled;
        //Gestion de niveles de la IA
        if (reloj.relojOn && !IAexiste)
        {
            IAexiste = true;
            IAcopia = Instantiate(IA, spawnIA.position, Quaternion.Euler(0, -90, 0));
            IAcomp = IAcopia.GetComponent<IAComportamiento>();
            if (reloj.tiempoRestante <= tiempo / 3 && reloj.tiempoRestante > 0)
            {
                level = 3;
                txt_level.text = "Nivel 3";
            }
            else if (reloj.tiempoRestante <= tiempo * 2 / 3 && reloj.tiempoRestante > tiempo / 3)
            {
                level = 2;
                txt_level.text = "Nivel 2";
            }
            else
            {
                level = 1;
                txt_level.text = "Nivel 1";
            }
        }

        if (IAcomp.pedidoHecho)
        {
            if (txt_animacion.transform.position != animacion_final.position)
            {
                txt_animacion.enabled = true;
                txt_animacion.transform.position = Vector3.MoveTowards(txt_animacion.transform.position, animacion_final.position, vel_animacion * Time.deltaTime);
            }
            else
            {
                txt_animacion.enabled = false;
                if (puntos == 0)
                {
                    txt_puntos.text = "000";
                }
                else
                {
                    txt_puntos.text = puntos.ToString();
                }
            }
        }
        else
        {
            txt_animacion.transform.position = animacion_inicio.position;
            txt_animacion.enabled = false;
        }

        //Desactivar controles táctiles cuando no hay pantalla táctil
        if (es_tactil)
        {
            controles.SetActive(true);
        }
        else
        {
            controles.SetActive(false);
        }

        //Gestion del tiempo
        if (reloj.final)
        {
            IAcomp.finTiempo = true;
        }
        else
        {
            IAcomp.finTiempo = false;
        }

        //Comportamiento del botón Menu  ---  Entra al menú y pausa el juego
        if (b_menu.pressed || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!es_tactil)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            Time.timeScale = 0;
            minion.se_mueve = false;
            menu.SetActive(true);
            controles.SetActive(false);
        }

        //Comportamiento del botón Atrás  ---  Sale del menú y reinicia el juego
        if (b_atras.pressed)
        {
            if (!es_tactil)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Time.timeScale = 1;
            minion.se_mueve = true;
            menu.SetActive(false);
            controles.SetActive(true);
        }

        //Comportamiento del botón Salir  ---  Cierra el juego
        if (b_salir.pressed)
        {
            Application.Quit();
        }

        //Recogida de valores del Menú
        minion.f_sensibilitat_mouse = s_sensi.value;
        txt_sensi.text = s_sensi.value.ToString();
        minion.f_impuls_bot = s_jump.value;
        txt_jump.text = s_jump.value.ToString();
        tiempo = s_time.value;
        txt_time.text = s_time.value.ToString();
        
    }


}
