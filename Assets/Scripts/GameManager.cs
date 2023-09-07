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
    private int vidas = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! Mas de un GameManager en escena.");
        }
    }




    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        hud.ActualizarPuntos(PuntosTotales);
    }

    public void PerderVidas()
    {
        vidas -= 1;


        if(vidas == 0)
        {
            //Reiniciar Nivel
            SceneManager.LoadScene(0);
        }
        hud.DesactivarVida(vidas);
    }

    public bool RecuperarVida()
    {
        if (vidas == 3)
        {
            return false;
        }
        hud.ActivarVida(vidas);
        vidas += 1;
        return true;
       
    }
}
