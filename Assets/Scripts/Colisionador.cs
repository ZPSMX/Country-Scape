using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Colisionador : MonoBehaviour
{
    public BoxCollider2D colisionDebajo;
    public ControladorDatosJuego controlador;


    private void Start()
    {
        
    }

    void Update()
    {
        colisionDebajo = GetComponent<BoxCollider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            controlador.CargarDatos();
            //SceneManager.LoadScene("Tutorial");
    }


}
