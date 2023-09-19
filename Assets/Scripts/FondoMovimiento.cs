using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [SerializeField] private Vector2 VelocidadMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugarRB;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugarRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        offset =(jugarRB.velocity.x * 0.1f)* VelocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
