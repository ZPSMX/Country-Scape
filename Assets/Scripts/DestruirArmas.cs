using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirArmas : MonoBehaviour
{
    public BoxCollider2D destruir;

    private void Update()
    {
        destruir = GetComponent<BoxCollider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
