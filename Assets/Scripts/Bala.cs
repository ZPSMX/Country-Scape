using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float da�o;

    private void Update()
    {
        transform.Translate(Vector2.right* velocidad*Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Enemigo>().TomarDa�o(da�o);
            Destroy(gameObject);
        }
    }
}
