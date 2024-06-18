using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{

   [SerializeField] private AudioClip SonidoMenu;
    public GameObject PanelOpciones;
    public GameObject PanelIncio;
    public GameObject PanelNivel;


    private void Start()
    {
        ControladroSonido.Instance.EjecutarSonido(SonidoMenu);

    }
    public void ComenzarHistoria()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ControladroSonido.Instance.PararSonido(SonidoMenu);
    } 

    public void menuOpciones()
    {
        PanelOpciones.SetActive(true);
        PanelIncio.SetActive(false);
        PanelNivel.SetActive(false);
    }

    public void menuNiveles()
    {
        PanelOpciones.SetActive(false);
        PanelIncio.SetActive(false);
        PanelNivel.SetActive(true);
    }

    public void regresoMenu()
    {
        PanelOpciones.SetActive(false);
        PanelIncio.SetActive(true);
        PanelNivel.SetActive(false);
    }
    
}
