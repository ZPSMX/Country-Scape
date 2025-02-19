using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutearMenu : MonoBehaviour
{
    private bool isMuted;
    public Button muteButton;

    void Start()
    {
        // Leer el estado real del volumen al inicio
        isMuted = AudioListener.volume == 0;
        muteButton.onClick.AddListener(ToggleMute);
    }

    void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
    }
}
