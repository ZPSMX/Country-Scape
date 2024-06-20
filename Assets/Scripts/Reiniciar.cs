using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reiniciar : MonoBehaviour
{
  public void ReIn()
    {
        //pasamos el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;
        //asignamos la escena actual al reiniciar siempre sera la escena que se haya jugado
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1f;
    }

    public void SiguienteEscena()
    {
        //siempre cargara la escena siguiente al orden del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   
}
