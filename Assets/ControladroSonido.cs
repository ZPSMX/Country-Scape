using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ControladroSonido : MonoBehaviour
{
    public static ControladroSonido Instance;
    private AudioSource audioSource;
    public Slider volumeSlider;

    public string archivoDeGuardado;
    public DatosJuegos datosJuego = new DatosJuegos();

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        // Cargar los datos de volumen al iniciar el juego
        CargarDatosAudio();
    }

    private void Start()
    {
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(CambiarVolumen);
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

    void CambiarVolumen(float volume)
    {
        AudioListener.volume = volume;
    }

    public void CargarDatosAudio()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);
            volumeSlider.value = datosJuego.volumen;
            AudioListener.volume = datosJuego.volumen; // Asegurarse de que el AudioListener también se actualice
        }
        else
        {
            Debug.Log("El archivo no existe");
        }
    }

    public void GuardarDatosAudio()
    {
        DatosJuegos nuevosDatos = new DatosJuegos()
        {
            volumen = volumeSlider.value
        };

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);
        Debug.Log("Archivo guardado");
    }

    // Método para asignar al botón de guardar
    public void OnGuardarButtonClicked()
    {
        GuardarDatosAudio();
    }
}
