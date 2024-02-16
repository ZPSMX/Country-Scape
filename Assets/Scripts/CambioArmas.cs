using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioArmas : MonoBehaviour
{
    [SerializeField] GameObject pistola;
    [SerializeField] GameObject metralleta;
    [SerializeField] GameObject escopeta;

    public BoxCollider2D consumible;

    private void Update()
    {
        consumible = GetComponent<BoxCollider2D>();

    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "M4")
        {
            metralleta.SetActive(true);
            pistola.SetActive(false);
            escopeta.SetActive(false);
           
        }
        else if (collision.gameObject.tag == "ShotGun")
        {
            escopeta.SetActive(true);
            pistola.SetActive(false);
            metralleta.SetActive(false);
           

        }

    }

}
