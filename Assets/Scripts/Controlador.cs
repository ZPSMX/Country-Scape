using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float Fuerzasalto;
    [SerializeField] private LayerMask CapaSuelo;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private AudioClip saltoSonido;
    [SerializeField] private float fuerzaGolpe;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private bool mirandoDerecha = true;
    private bool puedeMoverse = true;

    private Disparo disparoScript; // Referencia al script de disparo

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        // Obtener referencia al script Disparo en el mismo GameObject
        disparoScript = GetComponent<Disparo>();
    }

    void Update()
    {
        ProcesarMovimiento();
        ProcesarEntradaPC();
    }

    bool EstaEnSuelo()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.2f, CapaSuelo);
    }

    void Saltar()
    {
        if (!puedeMoverse || !EstaEnSuelo()) return;

        rigidBody.AddForce(Vector2.up * Fuerzasalto, ForceMode2D.Impulse);
        ControladroSonido.Instance.EjecutarSonido(saltoSonido);
    }

    void ProcesarMovimiento()
    {
        if (!puedeMoverse) return;

        // Movimiento en PC con A/D
        float movimientoPC = Input.GetAxis("Horizontal");

        // Movimiento en Android con Joystick
        float movimientoAndroid = joystick.Horizontal;

        // Priorizar joystick si está en uso, sino usar el teclado
        float movimientoFinal = movimientoAndroid != 0 ? movimientoAndroid : movimientoPC;

        rigidBody.velocity = new Vector2(movimientoFinal * velocidad, rigidBody.velocity.y);

        bool estaCorriendo = movimientoFinal != 0;
        animator.SetBool("isRunning", estaCorriendo);

        if (estaCorriendo && !particulas.isPlaying) particulas.Play();
        else if (!estaCorriendo && particulas.isPlaying) particulas.Stop();

        GestionarOrientacion(movimientoFinal);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((mirandoDerecha && inputMovimiento < 0) || (!mirandoDerecha && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void androidSaltar()
    {
        Saltar();
    }

    public void AplicarGolpe()
    {
        puedeMoverse = false;
        Vector2 direccionGolpe = joystick.Horizontal > 0 ? new Vector2(-1, 1) : new Vector2(1, 1);
        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);
        StartCoroutine(EsperarYActivarMovimiento());
    }

    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.1f);
        while (!EstaEnSuelo()) yield return null;
        puedeMoverse = true;
    }

    void ProcesarEntradaPC()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }

        if (Input.GetMouseButtonDown(0)) // Click izquierdo PRESIONADO
        {
            disparoScript.DispararSi();
        }

        if (Input.GetMouseButtonUp(0)) // Click izquierdo SOLTADO
        {
            disparoScript.DispararNo();
        }
    }
}
