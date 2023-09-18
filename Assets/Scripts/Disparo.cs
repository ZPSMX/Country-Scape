using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    public Animator animator;
    private bool disparoActivo;

    public void DispararSi()
    {
        disparoActivo = true;
        if (disparoActivo == true)
        {
            Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
            animator.SetBool("Disparar", true);
            
           
        }
    }

    public void DispararNo()
    {
        disparoActivo = false;
        if (disparoActivo == false)
        {
            animator.SetBool("Disparar", false);
           
        }
    }
}



