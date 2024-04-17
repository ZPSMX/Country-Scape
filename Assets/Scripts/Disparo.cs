using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [Header("GameObject que controla el disparo")]
    [SerializeField] private Transform controladorDisparo;


    //sprites balas
    [Header("Prefabs Balas")]
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject balaEscopeta;
    [SerializeField] private GameObject balaMetralleta;

    // sprites armas
    [Header("GameObjects SpriteArmas")]
    [SerializeField] GameObject pistola;
    [SerializeField] GameObject metralleta;
    [SerializeField] GameObject escopeta;

    // sprites hud armas
    [Header("Sprites de armas HUD")]
    [SerializeField] GameObject pistolaHUD;
    [SerializeField] GameObject metralletaHUD;
    [SerializeField] GameObject escopetaHUD;

    [Header("----Sprites Armas HUD----")]
    [SerializeField] GameObject Pistola;
    [SerializeField] GameObject Metralleta;
    [SerializeField] GameObject Escopeta;
    public TextMeshProUGUI Textobalas;

    public float ContadorBalaEscopeta = 15f;
    public float ContadorBalaMetralleta = 120f;
    
    [Header("Otros")]

    //box colider sprite arma consumible
    public BoxCollider2D consumible;

    public Animator animator;
    public Animator restarbalas;
    private bool disparoActivo;

    [Header("Frecuencia de Disparo de la metralleta")]
    public float frecuenciaDisparo;

    private void Update()
    {
        consumible = GetComponent<BoxCollider2D>();
        armasActivas();
        

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
            
            metralletaHUD.SetActive(true);
            pistolaHUD.SetActive(false);
            escopetaHUD.SetActive(false);
        }
        else if (collision.gameObject.tag == "ShotGun")
        {
            escopeta.SetActive(true);
            pistola.SetActive(false);
            metralleta.SetActive(false);

            metralletaHUD.SetActive(false);
            pistolaHUD.SetActive(false);
            escopetaHUD.SetActive(true);
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
            restarbalas.SetTrigger("RestarSi");

            //agregar a las animaciones el parametro trigger para desactivar inmediatamente al finalizar la animacion.
        }

        else if (disparoActivo == true && metralleta.activeSelf)
        {

            InvokeRepeating("metralletaOn", 0f, frecuenciaDisparo);
            restarbalas.SetTrigger("RestarSi");


        }
        else if (disparoActivo == true && escopeta.activeSelf)
        {
            Instantiate(balaEscopeta, controladorDisparo.position, controladorDisparo.rotation);
            animator.SetBool("Disparar", true);
            ContadorBalaEscopeta--;
            restarbalas.SetTrigger("RestarSi");

        }
    }

    public void DispararNo()
    {
        disparoActivo = false;
        if (disparoActivo == false )
        {
            animator.SetBool("Disparar", false);
            CancelInvoke("metralletaOn");
            
            
        }
    }

    //metodo aparte para definir disparo en automatico de metralleta y el tiempo en que tarda salir cada bala del cañon
    void metralletaOn()
    {
        Instantiate(balaMetralleta, controladorDisparo.position, controladorDisparo.rotation);
        restarbalas.SetTrigger("RestarSi");
        ContadorBalaMetralleta--;
        
    }

   


    public void armasActivas()
    {
        if (Pistola.activeSelf)
        {

            Textobalas.text = "999";
        }
        else if (Metralleta.activeSelf)
        {

            //se pasa valores de float contador Escopeta a string para verlo en hud
            Textobalas.text = ContadorBalaMetralleta.ToString();
            // condicion si contador es 0 entonces volver a pistola y reiniciar valores de balasEscopeta
            if (ContadorBalaMetralleta == 0)
            {
                //se desactiva la arma actual y se activa pistola de mano infinita
                pistola.SetActive(true);
                metralleta.SetActive(false);
                //reinicio de contador balas
                pistolaHUD.SetActive(true);
                metralletaHUD.SetActive(false);
                CancelInvoke("metralletaOn");
                ContadorBalaMetralleta = 120;


            }

        }
        else if (Escopeta.activeSelf)
        {
            //se pasa valores de float contador Escopeta a string para verlo en hud
            Textobalas.text = ContadorBalaEscopeta.ToString();
            // condicion si contador es 0 entonces volver a pistola y reiniciar valores de balasEscopeta
            if(ContadorBalaEscopeta == 0)
            {
                //se desactiva la arma actual y se activa pistola de mano infinita
                pistola.SetActive(true);
                escopeta.SetActive(false);
                //reinicio de contador balas
                pistolaHUD.SetActive(true);
                escopetaHUD.SetActive(false);
                ContadorBalaEscopeta = 15;

                
            }
        }


    }
}



