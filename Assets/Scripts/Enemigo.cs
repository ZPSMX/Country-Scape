using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemigo : MonoBehaviour
{
    public GameObject TextoDañoPrefab;
    private bool puedeAtacar = true;
    public float cooldownAtaque;
    private SpriteRenderer spriteRenderer;
    public ParticleSystem particulasEnemigo;
    public TMP_Text TextoPop;

   [SerializeField] private float vida;
    private GameObject efectoMuerte;
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        
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
            particulasEnemigo.Play();
            

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

    public void TomarDaño(float daño)
    {
        vida -= daño;
        TextoPop.text = "-"+daño.ToString();
        Instantiate(TextoDañoPrefab, transform.position, Quaternion.identity);

        if(vida<=0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }


}
