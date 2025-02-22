using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelFinal : MonoBehaviour
{
    public BoxCollider2D colisionDebajo;
    public GameObject panelUI;
    public TextMeshProUGUI puntosFinalesText; // Referencia al TextMeshProUGUI donde se mostrarán los puntos

    private void Update()
    {
        colisionDebajo = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panelUI.SetActive(true);
            MostrarPuntosFinales();
        }
    }

    private void MostrarPuntosFinales()
    {
        if (GameManager.Instance != null && puntosFinalesText != null)
        {
            int puntosMultiplicados = GameManager.Instance.PuntosTotales * 5000;
            puntosFinalesText.text = "Puntos Finales: " + puntosMultiplicados;
        }
        else
        {
            Debug.LogError("GameManager o el TextMeshProUGUI no están asignados en PanelFinal.");
        }
    }
}
