using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorHistoria : MonoBehaviour
{

    public GameObject comic1;
    public GameObject comic2;
    public GameObject comic3;
    public GameObject comic4;
    public GameObject comic5;


    public void comicDos()
    {
        comic2.SetActive(true);
        comic1.SetActive(false);
    }

    public void comicTres()
    {
        comic3.SetActive(true);
        comic2.SetActive(false);
    }
    public void comicCuatro()
    {
        comic4.SetActive(true);
        comic3.SetActive(false);
    }
    public void comicCinco()
    {
        comic5.SetActive(true);
        comic4.SetActive(false);
    }

    public void salida()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
