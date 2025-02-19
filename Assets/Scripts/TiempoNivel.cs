using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TiempoNivel : MonoBehaviour
{

    public float tiempoRestante = 60f; // 1 minuto
    public TextMeshProUGUI textoTiempo;

    void Update()
    {
        tiempoRestante -= Time.deltaTime; // Reducir el tiempo
        textoTiempo.text = "Tiempo: " + Mathf.Ceil(tiempoRestante) + "s"; // Actualizar UI

        if (tiempoRestante <= 0)
        {
            ReiniciarNivel();
        }
    }

    void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recargar la escena actual
    }
}

