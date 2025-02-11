using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public HUD hud;
    public int PuntosTotales { get { return puntosTotales; } }

    private int puntosTotales = 0;
    private int vidas;
    private int vidasIniciales = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            vidas = vidasIniciales;
            Debug.Log("GameManager iniciado correctamente.");
        }
        else
        {
            Debug.LogWarning("Se intentó crear un GameManager duplicado, destruyéndolo.");
            Destroy(gameObject);
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
        StartCoroutine(ActualizarHUD()); // Retrasar la asignación para evitar errores de carga
    }

    private IEnumerator ActualizarHUD()
    {
        yield return new WaitForSeconds(0.1f); // Breve espera para que el HUD cargue completamente
        hud = FindObjectOfType<HUD>(); // Buscar el nuevo HUD en la escena

        if (hud != null)
        {
            Debug.Log("HUD encontrado y referenciado correctamente.");
            hud.gameManager = this;
            hud.ActualizarPuntos(puntosTotales);
            ReiniciarVidas();
        }
        else
        {
            Debug.LogWarning("No se encontró HUD en la nueva escena.");
        }
    }


    private IEnumerator ActualizarReferenciasHUD()
    {
        yield return new WaitForEndOfFrame(); // Esperamos un frame para asegurar que el HUD esté listo
        hud = FindObjectOfType<HUD>(); // Se vuelve a buscar el HUD en la nueva escena

        if (hud != null)
        {
            Debug.Log("HUD encontrado y referenciado.");
            hud.gameManager = this;
            hud.ActualizarPuntos(puntosTotales);
            ReiniciarVidas();
        }
        else
        {
            Debug.LogWarning("HUD no encontrado en la nueva escena.");
        }
    }

    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log("Puntos sumados: " + puntosASumar + ", Total: " + puntosTotales);

        if (hud == null)
        {
            Debug.LogWarning("HUD no asignado. Intentando encontrarlo...");
            hud = FindObjectOfType<HUD>(); // Reasignar si se perdió la referencia
        }

        if (hud != null)
        {
            hud.ActualizarPuntos(puntosTotales);
            Debug.Log("Se llamó a ActualizarPuntos() en el HUD.");
        }
        else
        {
            Debug.LogError("HUD sigue sin encontrarse al intentar actualizar puntos.");
        }
    }


    public void PerderVidas()
    {
        vidas -= 1;

        if (vidas <= 0)
        {
            int escenaActual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(escenaActual);
            vidas = vidasIniciales;
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

    public void CambiarEscena(int escenaIndex)
    {
        Debug.Log("Cambiando a la escena: " + escenaIndex);
        SceneManager.LoadScene(escenaIndex);
    }
}
