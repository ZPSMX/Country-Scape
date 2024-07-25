using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public HUD hud;
    public int PuntosTotales { get { return puntosTotales; } }

    private int puntosTotales;
    private int vidas;
    private int vidasIniciales = 3; // Número inicial de vidas

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            vidas = vidasIniciales; // Inicializar vidas
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Cuidado! Más de un GameManager en escena.");
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hud = FindObjectOfType<HUD>();
        if (hud != null)
        {
            Debug.Log("HUD encontrado y referenciado.");
            ReiniciarVidas();
        }
        else
        {
            Debug.LogWarning("HUD no encontrado en la escena.");
        }
    }

    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        if (hud != null)
        {
            hud.ActualizarPuntos(PuntosTotales);
        }
    }

    public void PerderVidas()
    {
        vidas -= 1;

        if (vidas <= 0)
        {
            // Reiniciar nivel actual
            int escenaActual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(escenaActual);
            vidas = vidasIniciales; // Reiniciar vidas
        }
        else
        {
            if (hud != null)
            {
                hud.DesactivarVida(vidas);
            }
            else
            {
                Debug.LogWarning("HUD no está asignado o ha sido destruido.");
            }
        }
    }

    public bool RecuperarVida()
    {
        if (vidas >= vidasIniciales)
        {
            return false;
        }

        if (hud != null)
        {
            hud.ActivarVida(vidas);
        }
        vidas += 1;
        return true;
    }

    private void ReiniciarVidas()
    {
        vidas = vidasIniciales;
        for (int i = 0; i < vidas; i++)
        {
            hud.ActivarVida(i);
        }
    }
}
