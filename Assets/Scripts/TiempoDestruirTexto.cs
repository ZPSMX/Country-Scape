using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoDestruirTexto : MonoBehaviour
{

    public float tiempoDestruccion;
    private float temporizador;
    // Start is called before the first frame update
    void Start()
    {
        temporizador = tiempoDestruccion;
    }

    // Update is called once per frame
    void Update()
    {
        temporizador -= Time.deltaTime;
        if(temporizador <= 0)
        {
            Destroy(gameObject);
        }
    }
}
