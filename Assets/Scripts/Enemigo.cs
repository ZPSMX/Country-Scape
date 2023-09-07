using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    private bool puedeAtacar = true;
    public float cooldownAtaque;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (!puedeAtacar) return;

        puedeAtacar = false;

            // Cambiamos la opacidad del sprite.
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            GameManager.Instance.PerderVidas();
            //aplicamos golpe al personaje
            other.gameObject.GetComponent<Controlador>().AplicarGolpe();

             Invoke("ReactivarAtaque", cooldownAtaque);
        } 
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;

        // Cambiamos la opacidad del sprite.
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }
}
