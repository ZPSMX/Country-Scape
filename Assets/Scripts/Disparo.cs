using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    
    [SerializeField] private Transform controladorDisparo;
    //sprites balas
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject balaEscopeta;
    [SerializeField] private GameObject balaMetralleta;
    
    // sprites armas
    [SerializeField] GameObject pistola;
    [SerializeField] GameObject metralleta;
    [SerializeField] GameObject escopeta;

    //box colider sprite arma consumible
    public BoxCollider2D consumible;

    public Animator animator;
    private bool disparoActivo;

    private void Update()
    {
        consumible = GetComponent<BoxCollider2D>(); 
    }

    /**deteccion por colision de que objeto se esta colisionando y se aplica el cambio
    de sprite y desactivado de los otros en escena**/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "M4")
        {
            metralleta.SetActive(true) ;
            pistola.SetActive(false);
            escopeta.SetActive(false);
        }
        else if (collision.gameObject.tag == "ShotGun")
        {
            escopeta.SetActive(true);
            pistola.SetActive(false);
            metralleta.SetActive(false);
        }
    }
    //funcion boton disparo
    public void DispararSi()
    {
        //condicion si boton de disparo presionado se desencadena funcion de disparo
        //con parametros de la bala
        disparoActivo = true;
        //condicion si la pistola esta activa o la arma que se selecciono llamara al tipo de Gameobject bala que se solicita
        if (disparoActivo == true && pistola.activeSelf)
        {
            //instancia de cada gameobject de bala con su propio dano definidos en el inspector
            Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
            animator.SetBool("Disparar", true);
        }
        
        else if (disparoActivo == true && metralleta.activeSelf)
        {
            Instantiate(balaMetralleta, controladorDisparo.position, controladorDisparo.rotation);
            animator.SetBool("Disparar", true);
        }
        else if (disparoActivo == true && escopeta.activeSelf)
        {
            Instantiate(balaEscopeta, controladorDisparo.position, controladorDisparo.rotation);
            animator.SetBool("Disparar", true);
        }
    }

    public void DispararNo()
    {
        disparoActivo = false;
        if (disparoActivo == false)
        {
            animator.SetBool("Disparar", false);
           
        }
    }
}



