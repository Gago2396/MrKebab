using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelojIA : MonoBehaviour
{
    public float tiempoRestante;
    public bool relojOn = false;
    public bool final = false;

    //public TMP_Text RelojMuestra;

    public void inicio(float tiempoInicio)
    {
        tiempoRestante = tiempoInicio;
        relojOn = true;
        final = false;
    }

    void Update()
    {
        if (relojOn)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                updateReloj(tiempoRestante);
                if (tiempoRestante < 3)
                {
                    //RelojMuestra.color = Color.red;
                }
                else
                {
                    //RelojMuestra.color = Color.white;
                }
            }
            else
            {
                Debug.Log("Time is UP!");
                tiempoRestante = 0;
                relojOn = false;
                final = true;
                Invoke("desactivarReloj", 2);
            }
        }
    }

    void updateReloj(float tiempo)
    {
        tiempo += 1;

        float minutos = Mathf.FloorToInt(tiempo / 60);
        float segundos = Mathf.FloorToInt(tiempo % 60);

        //RelojMuestra.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void desactivarReloj()
    {
        //RelojMuestra.text = "";
    }

}