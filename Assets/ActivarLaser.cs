using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarLaser : MonoBehaviour
{

    public float TiempoActivarLaser;
    public GameObject Laser;
    public ParticleSystem particulasLaser;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnLaser());
        particulasLaser.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OnLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(TiempoActivarLaser);
            Laser.SetActive(!Laser.activeSelf);
            particulasLaser.Play();


        }

        

    }
}
