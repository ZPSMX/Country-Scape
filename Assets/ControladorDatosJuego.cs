using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public DatosJuegos datosJuego = new DatosJuegos();


    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";

        jugador = GameObject.FindGameObjectWithTag("Player");

    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }



    public void CargarDatos()
    {
        if(File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuegos>(contenido);
            Debug.Log("Posicion Jugador" + datosJuego.posicion);

            jugador.transform.position = datosJuego.posicion;
        }
        else { Debug.Log("El archivo no existe"); }
    }

    private void GuardarDatos()
    {
        DatosJuegos nuevosDatos = new DatosJuegos()
        { posicion = jugador.transform.position };
       

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);
        Debug.Log("archivo guardado");

    }

}
