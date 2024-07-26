using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ScriptMenu : MonoBehaviour
{
    [SerializeField] private AudioClip SonidoMenu;
    public GameObject PanelOpciones;
    public GameObject PanelIncio;
    public GameObject PanelNivel;
    private string archivoDeGuardado;

    private void Start()
    {
        ControladroSonido.Instance.EjecutarSonido(SonidoMenu);
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";
    }

    public void ComenzarHistoria()
    {
        // Leer el JSON de ControladorDatosJuego
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            DatosJuegos datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);

            // Verificar si la escena guardada es válida
            if (!string.IsNullOrEmpty(datosJuego.escena) && SceneExists(datosJuego.escena))
            {
                // Cargar la última escena guardada
                SceneManager.LoadScene(datosJuego.escena);
            }
            else
            {
                // Cargar la siguiente escena en la jerarquía
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            // Cargar la siguiente escena en la jerarquía si no hay datos guardados
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

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

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}


