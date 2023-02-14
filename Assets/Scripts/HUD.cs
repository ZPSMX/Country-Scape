using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;

    private void Update()
    {
        puntos.text = gameManager.PuntosTotales.ToString();
    }


}
