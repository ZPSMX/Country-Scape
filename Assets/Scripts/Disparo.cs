using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject proyectil;
    public float velocidadDisparo = 10f;

    private Vector2 direccionDisparo;

    Controlador controlador;
    

     void Start()
    {
        controlador= GetComponent<Controlador>();
    }

    void Update(){
       
        if (Input.GetButtonDown("Fire1")) 
        {
            Disparar(controlador.joystick.Horizontal);

        }


        void Disparar(float disparo)
        {

            if (controlador.gameObject.transform.localScale.x == 4)
            {
                direccionDisparo = Vector2.right;
            }
            else if (controlador.gameObject.transform.localScale.x == -4)
            {
                direccionDisparo = Vector2.left;
            }
            GameObject proyectilInstancia = Instantiate(proyectil, transform.position, Quaternion.identity);
            Rigidbody2D rb = proyectilInstancia.GetComponent<Rigidbody2D>();
            rb.velocity = direccionDisparo * velocidadDisparo;
           
        }

    }
}

