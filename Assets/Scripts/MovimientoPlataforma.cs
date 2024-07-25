using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    public float velocidad;
    public Transform controlador; // Transform para ambas detecciones
    public float distanciaSuelo; // Distancia para detectar el suelo
    public float distanciaFrontal; // Distancia para detectar la plataforma al frente
    public bool moviendoDerecha;
    private Rigidbody2D rb;

    // Tags de las armas que queremos ignorar
    private string[] tagsIgnorados = { "M4", "ShotGun" };

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controlador.position, Vector2.down, distanciaSuelo);

        // Determinar dirección horizontal según el movimiento
        Vector2 direccionHorizontal = moviendoDerecha ? Vector2.right : Vector2.left;
        RaycastHit2D informacionFrontal = Physics2D.Raycast(controlador.position, direccionHorizontal, distanciaFrontal);

        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        // Cambiar de dirección si no hay suelo debajo
        if (!informacionSuelo)
        {
            Girar();
        }
        // Cambiar de dirección si hay una plataforma al frente, excepto si es un arma ignorada
        else if (informacionFrontal && !EsArmaIgnorada(informacionFrontal.collider))
        {
            Girar();
        }
    }

    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private bool EsArmaIgnorada(Collider2D collider)
    {
        foreach (string tag in tagsIgnorados)
        {
            if (collider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controlador.transform.position, controlador.transform.position + Vector3.down * distanciaSuelo);

        // Gizmo para la verificación frontal
        Vector3 direccionHorizontal = moviendoDerecha ? Vector3.right : Vector3.left;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(controlador.transform.position, controlador.transform.position + direccionHorizontal * distanciaFrontal);
    }
}

