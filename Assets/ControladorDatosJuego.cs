using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public DatosJuegos datosJuego = new DatosJuegos();
    public Button botonReiniciar; // Botón para reiniciar
    public Button botonIniciar; // Botón para iniciar el juego

    private void Start()
    {
        if (botonReiniciar != null)
        {
            botonReiniciar.onClick.AddListener(ReiniciarPosicion);
        }
        if (botonIniciar != null)
        {
            botonIniciar.onClick.AddListener(IniciarJuego);
        }

        // Solo guardar posición inicial si no estamos en la escena Intro o Menu
        if (!EsEscenaIntroOMenu())
        {
            jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                GuardarPosicionInicial();
            }
            else
            {
                Debug.LogWarning("Jugador no encontrado. Asegúrate de que el jugador tiene el tag 'Player'.");
            }
        }
    }

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";
    }

    private void GuardarPosicionInicial()
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

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);
        Debug.Log("Posición inicial guardada");
    }

    public void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);
            Debug.Log("Posición Jugador: " + datosJuego.posicion);

            jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                jugador.transform.position = datosJuego.posicion;
            }
            else
            {
                Debug.LogWarning("Jugador no encontrado al cargar datos.");
            }
        }
        else
        {
            Debug.Log("El archivo no existe");
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

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);
        Debug.Log("Archivo guardado");
    }

    private void ReiniciarPosicion()
    {
        CargarDatos();
        Debug.Log("Posición reiniciada");
    }

    private void IniciarJuego()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);
            Debug.Log("Última escena jugada: " + datosJuego.escena);

            // Cargar la última escena jugada si no es Intro o Menu
            if (datosJuego.escena != "Intro" && datosJuego.escena != "Menu")
            {
                SceneManager.LoadScene(datosJuego.escena);
            }
            else
            {
                Debug.Log("Última escena guardada es Intro o Menu, cargando la primera escena del juego.");
                SceneManager.LoadScene("NombreDeTuPrimeraEscena"); // Cambia esto por el nombre de tu primera escena
            }
        }
        else
        {
            Debug.Log("No hay datos guardados. Cargando la primera escena.");
            // Cargar la primera escena del juego si no hay datos guardados
            SceneManager.LoadScene("NombreDeTuPrimeraEscena"); // Cambia esto por el nombre de tu primera escena
        }
    }

    private bool EsEscenaIntroOMenu()
    {
        string escenaActual = SceneManager.GetActiveScene().name;
        return escenaActual == "Intro" || escenaActual == "Menu";
    }
}





