using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionadoresTutorial : MonoBehaviour
{

    [Header("Colisionadores")]
    public Collider2D colisionadorUno;
    public Collider2D colisionadorDos;
    public Collider2D colisionadorTres;
    public Collider2D colisionadorCuatro;
    public Collider2D colisionadorCinco;
    public Collider2D colisionadorSeis;

    [Header("Textos")]
    public GameObject textoUno;
    public GameObject textoDos;
    public GameObject textoTres;
    public GameObject textoCuatro;
    public GameObject textoCinco;
    public GameObject textoSeis;



    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other == colisionadorUno) { textoUno.SetActive(true); }
        else if (other == colisionadorDos) { textoDos.SetActive(true); }
        else if (other == colisionadorTres) { textoTres.SetActive(true); }
        else if (other== colisionadorCuatro) { textoCuatro.SetActive(true); }
        else if (other == colisionadorCinco) { textoCinco.SetActive(true); }
        else if (other == colisionadorSeis) { textoSeis.SetActive(true); }
       

        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == colisionadorUno) { textoUno.SetActive(false); }
        else if (other == colisionadorDos) { textoDos.SetActive(false); }
        else if (other == colisionadorTres) { textoTres.SetActive(false); }
        else if (other== colisionadorCuatro) { textoCuatro.SetActive(false);}
        else if (other == colisionadorCinco) { textoCinco.SetActive(false); }
        else if (other== colisionadorSeis) { textoSeis.SetActive(false);}

    }

    
}
