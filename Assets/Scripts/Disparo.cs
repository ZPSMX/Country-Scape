using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject proyectil;
    public float velocidadDisparo = 10f;
    bool disparo = false;

    private Vector2 direccionDisparo;
    Controlador controlador;
    

     void Start()
    {
        controlador= GetComponent<Controlador>();
    }

    void Update(){
       
         
       /** if (Input.GetButtonDown("Fire1")) 
        {
            Disparar(controlador.joystick.Horizontal);

        }**/


    }
    public void Disparar()
    {

        disparo = true;
        if (controlador.gameObject.transform.localScale.x == 4 && disparo == true)
        {
            direccionDisparo = Vector2.right;
            disparo= false;
        }
        else if (controlador.gameObject.transform.localScale.x == -4 && disparo == true)
        {
            direccionDisparo = Vector2.left;
            disparo= false;
        }
        GameObject proyectilInstancia = Instantiate(proyectil, transform.position, Quaternion.identity);
        Rigidbody2D rb = proyectilInstancia.GetComponent<Rigidbody2D>();
        rb.velocity = direccionDisparo * velocidadDisparo;

    }
}

