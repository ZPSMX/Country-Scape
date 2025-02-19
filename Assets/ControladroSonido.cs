using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ControladroSonido : MonoBehaviour
{
    public static ControladroSonido Instance;
    private AudioSource audioSource;
    public Slider volumeSlider;

    public string archivoDeGuardado;
    public DatosJuegos datosJuego = new DatosJuegos();

    private void Awake()
    {
        archivoDeGuardado = Path.Combine(Application.persistentDataPath, "datosJuego.json");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento de carga de escena
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        // Cargar los datos de volumen al iniciar el juego
        //CargarDatosAudio();
    }

    private void Start()
    {
        FindVolumeSlider();
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(CambiarVolumen);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribirse del evento de carga de escena
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindVolumeSlider();
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(CambiarVolumen);
        }
    }

    private void FindVolumeSlider()
    {
        volumeSlider = FindInactiveObjectByName<Slider>("VolumeSlider");
        if (volumeSlider == null)
        {
            Debug.LogWarning("VolumeSlider no encontrado en la escena.");
        }
    }

    private T FindInactiveObjectByName<T>(string name) where T : Component
    {
        T[] objs = Resources.FindObjectsOfTypeAll<T>();
        foreach (T obj in objs)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    public void EjecutarSonido(AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);
    }

    public void PararSonido(AudioClip sonido)
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void CambiarVolumen(float volume)
    {
        AudioListener.volume = volume;
        //GuardarDatosAudio();
    }

    public void CargarDatosAudio()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);
            AudioListener.volume = datosJuego.volumen;
        }
        else
        {
            Debug.Log("El archivo no existe");
        }
    }

    public void GuardarDatosAudio()
    {
        if (File.Exists(archivoDeGuardado))
        {
            // Leer el JSON existente para no sobrescribir otros datos
            string contenido = File.ReadAllText(archivoDeGuardado);
            DatosJuegos datosExistentes = JsonUtility.FromJson<DatosJuegos>(contenido);
            datosExistentes.volumen = AudioListener.volume;

            // Guardar solo el volumen
            string cadenaJSON = JsonUtility.ToJson(datosExistentes);
            File.WriteAllText(archivoDeGuardado, cadenaJSON);
            Debug.Log("Volumen guardado");
        }
        else
        {
            // Si no existe el archivo, crearlo con los datos actuales
            DatosJuegos nuevosDatos = new DatosJuegos()
            {
                volumen = AudioListener.volume
            };

            string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
            File.WriteAllText(archivoDeGuardado, cadenaJSON);
            Debug.Log("Archivo creado y volumen guardado");
        }
    }
}

