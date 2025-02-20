using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [Header("GameObject que controla el disparo")]
    [SerializeField] private Transform controladorDisparo;

    // Sprites balas
    [Header("Prefabs Balas")]
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject balaEscopeta;
    [SerializeField] private GameObject balaMetralleta;

    // Sprites armas
    [Header("GameObjects SpriteArmas")]
    [SerializeField] GameObject pistola;
    [SerializeField] GameObject metralleta;
    [SerializeField] GameObject escopeta;

    // Sprites HUD armas
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
    public BoxCollider2D consumible;

    public Animator animatorPistola;
    public Animator animatorMetralleta;
    public Animator animatorEscopeta;
    public Animator restarbalas;
    private bool disparoActivo;

    [Header("Frecuencia de Disparo de la metralleta")]
    public float frecuenciaDisparo;

    [Header("Sonidos")]
    [SerializeField] private AudioClip pistolaClip;
    [SerializeField] private AudioClip EscopetaClip;
    [SerializeField] private AudioClip RifleClip;

    private void Update()
    {
        consumible = GetComponent<BoxCollider2D>();
        armasActivas();
    }

    /** Detección por colisión de qué objeto se está colisionando y se aplica el cambio 
     * de sprite y desactivado de los otros en escena **/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "M4")
        {
            metralleta.SetActive(true);
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

    // Función botón disparo
    public void DispararSi()
    {
        disparoActivo = true;

        // Disparo con pistola
        if (pistola.activeSelf)
        {
            Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
            animatorPistola.SetBool("Disparar", true);
            restarbalas.SetTrigger("RestarSi");
            ControladroSonido.Instance.EjecutarSonido(pistolaClip);
        }
        // Disparo automático con metralleta
        else if (metralleta.activeSelf)
        {
            InvokeRepeating("metralletaOn", 0f, frecuenciaDisparo);
            InvokeRepeating("ReproducirSonidoRifle", 0f, frecuenciaDisparo);
        }
        // Disparo con escopeta
        else if (escopeta.activeSelf)
        {
            Instantiate(balaEscopeta, controladorDisparo.position, controladorDisparo.rotation);
            animatorEscopeta.SetTrigger("Disparo");
            ContadorBalaEscopeta--;
            restarbalas.SetTrigger("RestarSi");
            ControladroSonido.Instance.EjecutarSonido(EscopetaClip);
        }
    }

    public void DispararNo()
    {
        disparoActivo = false;

        // Si se deja de disparar, cancelar la repetición de metralleta y su sonido
        if (!disparoActivo)
        {
            animatorPistola.SetBool("Disparar", false);
            CancelInvoke("metralletaOn");
            CancelInvoke("ReproducirSonidoRifle");
        }
    }

    // Método aparte para definir disparo en automático de metralleta y el tiempo en que tarda en salir cada bala del cañón
    void metralletaOn()
    {
        Instantiate(balaMetralleta, controladorDisparo.position, controladorDisparo.rotation);
        restarbalas.SetTrigger("RestarSi");
        animatorMetralleta.SetTrigger("Disparar");
        ContadorBalaMetralleta--;
    }

    // Método para reproducir el sonido de la metralleta en bucle
    void ReproducirSonidoRifle()
    {
        if (ControladroSonido.Instance != null)
        {
            ControladroSonido.Instance.EjecutarSonido(RifleClip);
        }
    }

    public void armasActivas()
    {
        if (Pistola.activeSelf)
        {
            Textobalas.text = "999";
        }
        else if (Metralleta.activeSelf)
        {
            // Se pasa valores de float contador Escopeta a string para verlo en HUD
            Textobalas.text = ContadorBalaMetralleta.ToString();
            // Condición si contador es 0 entonces volver a pistola y reiniciar valores de balasEscopeta
            if (ContadorBalaMetralleta == 0)
            {
                pistola.SetActive(true);
                metralleta.SetActive(false);
                pistolaHUD.SetActive(true);
                metralletaHUD.SetActive(false);
                CancelInvoke("metralletaOn");
                CancelInvoke("ReproducirSonidoRifle");
                ContadorBalaMetralleta = 120;
            }
        }
        else if (Escopeta.activeSelf)
        {
            // Se pasa valores de float contador Escopeta a string para verlo en HUD
            Textobalas.text = ContadorBalaEscopeta.ToString();
            // Condición si contador es 0 entonces volver a pistola y reiniciar valores de balasEscopeta
            if (ContadorBalaEscopeta == 0)
            {
                pistola.SetActive(true);
                escopeta.SetActive(false);
                pistolaHUD.SetActive(true);
                escopetaHUD.SetActive(false);
                ContadorBalaEscopeta = 15;
            }
        }
    }
}
