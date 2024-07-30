using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public DatosJuegos datosJuego = new DatosJuegos();

    private void Awake()
    {
        archivoDeGuardado = Path.Combine(Application.persistentDataPath, "datosJuego.json");
        Debug.Log("Ruta del archivo de guardado: " + archivoDeGuardado);
    }

    private void Start()
    {
        if (!EsEscenaIntroOMenu())
        {
            jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                GuardarDatos(); // Guardar posición inicial al iniciar
            }
            else
            {
                Debug.LogWarning("Jugador no encontrado. Asegúrate de que el jugador tiene el tag 'Player'.");
            }
        }
    }

    public void CargarDatos()
    {
        try
        {
            if (File.Exists(archivoDeGuardado))
            {
                string contenido = File.ReadAllText(archivoDeGuardado);
                datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);
                Debug.Log("Datos cargados: " + contenido);

                if (jugador == null)
                {
                    jugador = GameObject.FindGameObjectWithTag("Player");
                }

                if (jugador != null)
                {
                    jugador.transform.position = datosJuego.posicion;
                    Debug.Log("Posición del jugador establecida a: " + datosJuego.posicion);
                }
                else
                {
                    Debug.LogWarning("Jugador no encontrado al cargar datos.");
                }

                // Verificar si la escena guardada es válida y cargarla si es necesario
                if (!string.IsNullOrEmpty(datosJuego.escena) && SceneExists(datosJuego.escena))
                {
                    SceneManager.LoadScene(datosJuego.escena);
                }
                else
                {
                    Debug.LogWarning("Nombre de escena guardado no válido o vacío.");
                }
            }
            else
            {
                Debug.Log("El archivo no existe: " + archivoDeGuardado);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error cargando datos: " + e.Message);
        }
    }

    public void GuardarDatos()
    {
        if (EsEscenaIntroOMenu())
        {
            Debug.Log("No se guarda la posición en las escenas Intro o Menu.");
            return;
        }

        DatosJuegos nuevosDatos = new DatosJuegos()
        {
            posicion = jugador.transform.position,
            escena = SceneManager.GetActiveScene().name // Guardar el nombre de la escena actual
        };

        try
        {
            string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
            File.WriteAllText(archivoDeGuardado, cadenaJSON);
            Debug.Log("Datos guardados: " + cadenaJSON);
        }
        catch (Exception e)
        {
            Debug.LogError("Error guardando datos: " + e.Message);
        }
    }

    private bool EsEscenaIntroOMenu()
    {
        string escenaActual = SceneManager.GetActiveScene().name;
        return escenaActual == "Intro" || escenaActual == "Menu";
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