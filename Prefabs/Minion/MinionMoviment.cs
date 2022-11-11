using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionMoviment : MonoBehaviour
{
    public float f_velocitat = 1f;
    public float f_sensibilitat_mouse;
    public float f_impuls_bot;
    public bool se_mueve = true;
    public FixedJoystick joystickMov;
    public FixedJoystick joystickCam;
    public BotonComportamiento b_salta;

    Vector3 v3_l_velocitat = Vector3.zero;
    CharacterController cc_minion;
    Vector3 v3_g_normal = Vector3.up;
    
    Vector3 v3_posicio_inicial = Vector3.zero;
    Quaternion q_orientacio_inicial = Quaternion.identity;

    public float f_linia_vida = -10f;

    bool b_toca_sostre = false;

    private void Start()
    {
        cc_minion = GetComponent<CharacterController>();
        //b_salta.onClick.AddListener(jump);
        v3_posicio_inicial = transform.position;
        q_orientacio_inicial = transform.rotation;
        if (!Input.touchSupported)
            Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void LateUpdate()
    {        
        if (transform.position.y < f_linia_vida)
        {
            transform.position = v3_posicio_inicial;
            transform.rotation = q_orientacio_inicial;
            v3_l_velocitat = Vector3.zero;
            return;
        }

        RaycastHit rch;
        if (Physics.Raycast(transform.position,Vector3.down,out rch,cc_minion.height/2+0.2f))
        //if(cc_minion.isGrounded)
        {            
            GetComponent<Renderer>().material.color = Color.red;

            b_toca_sostre = false;
            v3_l_velocitat = Vector3.zero;
                                    
            //calcul direccio
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || joystickMov.Vertical > 0)
                v3_l_velocitat += Vector3.forward;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || joystickMov.Vertical < 0)
                v3_l_velocitat += Vector3.back;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || joystickMov.Horizontal > 0.2f)
                v3_l_velocitat += Vector3.right;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || joystickMov.Horizontal < -0.2f)
                v3_l_velocitat += Vector3.left;

            v3_l_velocitat = Vector3.ProjectOnPlane(v3_l_velocitat,
               transform.InverseTransformVector(rch.normal));

            v3_l_velocitat = v3_l_velocitat.normalized;

            // calcul tamany
            v3_l_velocitat = v3_l_velocitat * f_velocitat;

            if (Input.GetKey(KeyCode.Space) || b_salta.pressed)
            {
                jump();
            }
                

            Velocitat v = rch.transform.GetComponent<Velocitat>();
            if (v != null)
                v3_l_velocitat += transform.InverseTransformVector(v.v3_g_velocitat);

            //orientacio
            if (se_mueve)
            {
                if (Input.touchSupported)
                {
                    transform.Rotate(Vector3.up, joystickCam.Horizontal * f_sensibilitat_mouse, Space.Self);

                }
                else
                {
                    transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * f_sensibilitat_mouse * 2, Space.Self);
                }
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.yellow;

            if ((cc_minion.collisionFlags & CollisionFlags.Above) != 0 && !b_toca_sostre)
            {
                v3_l_velocitat = new Vector3(v3_l_velocitat.x/2f, -1.5f, v3_l_velocitat.z/2f);
                b_toca_sostre = true;
            }

            if ((cc_minion.collisionFlags & CollisionFlags.Sides) != 0)
            {
                v3_l_velocitat = new Vector3(-v3_l_velocitat.x/3f, 0f, -v3_l_velocitat.z /3f);
            }

            //gravetat de global a local
            v3_l_velocitat += transform.InverseTransformVector(Physics.gravity * Time.deltaTime);
        }

        v3_l_velocitat += transform.InverseTransformVector(v3_g_expolsio);
        v3_g_expolsio = Vector3.zero;


        //reescriure v3_l com a global
        Vector3 v3_g_velocitat = transform.TransformVector(v3_l_velocitat);


        //calcul espai
        cc_minion.Move(v3_g_velocitat * Time.deltaTime);
    }

    public void jump()
    {
        v3_l_velocitat += new Vector3(0f, f_impuls_bot, 0f);
    }

    Vector3 v3_g_expolsio = Vector3.zero;

    public void AddExplosionForce(float f_power,Vector3 v3_explosionPos,float f_radius,
        float f_modificador_vetical)
    {
        f_power /= 6;

        //direccio del vector
        v3_g_expolsio = (transform.position - v3_explosionPos).normalized;

        float f_impuls = f_power - (f_power / f_radius * Vector3.Distance(transform.position, v3_explosionPos));

        v3_g_expolsio *= f_impuls;

        v3_g_expolsio += new Vector3(0f, f_modificador_vetical*3, 0f);
    }


    public Vector3 Torna_Velocitat()
    {
        return transform.InverseTransformVector(v3_l_velocitat);
    }
}
