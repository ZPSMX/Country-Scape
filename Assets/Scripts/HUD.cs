using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI balas;
    public GameObject[] vida;
    public GameObject[] SpriteVida;
    public GameObject PanelEnJuego;

    private void Start()
    {
        StartCoroutine(EsperarGameManager());
    }

    private IEnumerator EsperarGameManager()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        gameManager = GameManager.Instance;
        gameManager.hud = this;
        ActualizarPuntos(gameManager.PuntosTotales);
        Debug.Log("HUD correctamente inicializado con puntuación: " + gameManager.PuntosTotales);
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            puntos.text = GameManager.Instance.PuntosTotales.ToString();
        }
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        if (puntos != null)
        {
            Debug.Log("Actualizando HUD con puntuación: " + puntosTotales);
            puntos.text = puntosTotales.ToString();
        }
        else
        {
            Debug.LogError("Referencia a Puntos en HUD es nula.");
        }
    }

    public void DesactivarVida(int indice)
    {
        vida[indice].SetActive(false);
        SpriteVida[indice].SetActive(true);
    }

    public void ActivarVida(int indice)
    {
        vida[indice].SetActive(true);
        SpriteVida[indice].SetActive(false);
    }

    public void ActivarPanelJuego()
    {
        PanelEnJuego.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DesactivarPanelJuego()
    {
        PanelEnJuego.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CargarMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
