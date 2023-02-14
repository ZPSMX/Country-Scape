using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Colisionador : MonoBehaviour
{
    public BoxCollider2D colisionDebajo;

    void Update()
    {
        colisionDebajo = GetComponent<BoxCollider2D>();  


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SceneManager.LoadScene("Tutorial");
    }


}
