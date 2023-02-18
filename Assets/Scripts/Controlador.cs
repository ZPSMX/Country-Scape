using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.IO;

public class Controlador : MonoBehaviour
{

    public float velocidad;
    public float Fuerzasalto;
    public LayerMask CapaSuelo;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    public bool mirandoDerecha = true;
    private Animator animator;
    [SerializeField] public FixedJoystick joystick;
    public bool botonSalto= false;


    


    // Update is called once per frame
    void Update()
    {

     
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        Debug.Log(joystick.Horizontal);
       
        ProcesarMovimiento();
        ProcesarSalto();
       

    }

    bool EstaenSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, CapaSuelo);
        return raycastHit.collider;
    }



    void ProcesarSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstaenSuelo())
        {
            rigidBody.AddForce(Vector2.up * Fuerzasalto, ForceMode2D.Impulse);
        }
    }
    void ProcesarMovimiento()
    {
        //Logica de Movimiento PC
        float inputMovimiento = Input.GetAxis("Horizontal");


        /** if (inputMovimiento != 0)
         {
             animator.SetBool("isRunning",true);
         }
         else { animator.SetBool("isRunning", false); }
        
         GestionarOrientacion(inputmovimiento);

        rigidBody.velocity = new Vector2(inputMovimiento * velocidad, rigidBody.velocity.y);**/

        //Logica de Movimiento ANDROID


        rigidBody.velocity = new Vector2(joystick.Horizontal * velocidad, rigidBody.velocity.y);

        if (joystick.Horizontal != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else { animator.SetBool("isRunning", false); }

        GestionarOrientacion(joystick.Horizontal);

        
    }

    void GestionarOrientacion(float inputmovimiento)
    {
        //Si se cumple condición
        if ((mirandoDerecha == true && inputmovimiento < 0) || (mirandoDerecha == false && inputmovimiento > 0))
        {
            //Ejecutar codigo de volteado 
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }

    public void androidSaltar() {
        
        botonSalto = true;

        if (botonSalto==true && EstaenSuelo())
        {
            rigidBody.AddForce(Vector2.up * Fuerzasalto, ForceMode2D.Impulse);
        }

    }

}
