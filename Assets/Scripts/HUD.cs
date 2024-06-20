using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI balas;
    public GameObject[] vida;
    public GameObject[] SpriteVida;
    public GameObject PanelEnJuego;



    private void Update()
    {
        puntos.text = GameManager.Instance.PuntosTotales.ToString();
        
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text += puntosTotales.ToString();
        
    }

    public void DesactivarVida(int indice)
    {
        vida[indice].gameObject.SetActive(false);
        SpriteVida[indice].gameObject.SetActive(true);
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
