using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deteccionUranio : MonoBehaviour
{
    public int valor = 1;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance; // Asigna la instancia del GameManager al inicio
        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en deteccionUranio. Verifica que existe en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.SumarPuntos(valor);
            }
            else
            {
                Debug.LogError("No se pudo sumar puntos porque GameManager es nulo.");
            }

            Destroy(this.gameObject);
        }
    }
}
