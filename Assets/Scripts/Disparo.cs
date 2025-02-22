using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private GameObject pistola;
    [SerializeField] private GameObject metralleta;
    [SerializeField] private GameObject escopeta;

    // Sprites HUD armas
    [Header("Sprites de armas HUD")]
    [SerializeField] private GameObject pistolaHUD;
    [SerializeField] private GameObject metralletaHUD;
    [SerializeField] private GameObject escopetaHUD;

    [Header("----Sprites Armas HUD----")]
    [SerializeField] private GameObject Pistola;
    [SerializeField] private GameObject Metralleta;
    [SerializeField] private GameObject Escopeta;
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
    private AudioClip pistolaClip;
    private AudioClip escopetaClip;
    private AudioClip rifleClip;

    private void Start()
    {
        // Cargar los clips de audio automáticamente desde Resources/Sonidos/
        pistolaClip = Resources.Load<AudioClip>("Sonidos/Pistola");
        escopetaClip = Resources.Load<AudioClip>("Sonidos/Escopeta");
        rifleClip = Resources.Load<AudioClip>("Sonidos/Rifle");

        // Verificación de que los clips se han cargado correctamente
        if (pistolaClip == null) Debug.LogError("No se pudo cargar el sonido Pistola. Verifica la ruta.");
        if (escopetaClip == null) Debug.LogError("No se pudo cargar el sonido Escopeta. Verifica la ruta.");
        if (rifleClip == null) Debug.LogError("No se pudo cargar el sonido Rifle. Verifica la ruta.");
    }

    private void Update()
    {
        consumible = GetComponent<BoxCollider2D>();
        armasActivas();
    }

    /** Detección por colisión de qué objeto se está colisionando y se aplica el cambio 
     * de sprite y desactivado de los otros en escena **/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("M4"))
        {
            metralleta.SetActive(true);
            pistola.SetActive(false);
            escopeta.SetActive(false);

            metralletaHUD.SetActive(true);
            pistolaHUD.SetActive(false);
            escopetaHUD.SetActive(false);
        }
        else if (collision.CompareTag("ShotGun"))
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
            InvokeRepeating(nameof(metralletaOn), 0f, frecuenciaDisparo);
            InvokeRepeating(nameof(ReproducirSonidoRifle), 0f, frecuenciaDisparo);
        }
        // Disparo con escopeta
        else if (escopeta.activeSelf)
        {
            Instantiate(balaEscopeta, controladorDisparo.position, controladorDisparo.rotation);
            animatorEscopeta.SetTrigger("Disparo");
            ContadorBalaEscopeta--;
            restarbalas.SetTrigger("RestarSi");
            ControladroSonido.Instance.EjecutarSonido(escopetaClip);
        }
    }

    public void DispararNo()
    {
        disparoActivo = false;

        // Si se deja de disparar, cancelar la repetición de metralleta y su sonido
        if (!disparoActivo)
        {
            animatorPistola.SetBool("Disparar", false);
            CancelInvoke(nameof(metralletaOn));
            CancelInvoke(nameof(ReproducirSonidoRifle));
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
            ControladroSonido.Instance.EjecutarSonido(rifleClip);
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
                CancelInvoke(nameof(metralletaOn));
                CancelInvoke(nameof(ReproducirSonidoRifle));
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
