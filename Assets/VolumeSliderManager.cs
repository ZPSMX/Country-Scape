using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (ControladroSonido.Instance != null)
        {
            // Cargar el volumen actual desde ControladroSonido
            volumeSlider.value = AudioListener.volume;

            // Añadir el listener para actualizar el volumen y guardar los datos cuando el slider cambie
            volumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
        }
    }

    void UpdateVolume()
    {
        if (ControladroSonido.Instance != null)
        {
            ControladroSonido.Instance.CambiarVolumen(volumeSlider.value);
            ControladroSonido.Instance.GuardarDatosAudio();
        }
    }
}
