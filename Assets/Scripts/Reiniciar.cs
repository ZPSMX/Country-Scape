using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reiniciar : MonoBehaviour
{
  public void ReIn()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
