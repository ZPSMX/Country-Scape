using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI balas;
    public GameObject[] vida;
    public GameObject[] SpriteVida;



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


}
