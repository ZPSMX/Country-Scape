using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelFinal : MonoBehaviour
{

    public BoxCollider2D colisionDebajo;
    public GameObject panelUI;

    void Update()
    {
        colisionDebajo = GetComponent<BoxCollider2D>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            panelUI.SetActive(true);
    }
}
