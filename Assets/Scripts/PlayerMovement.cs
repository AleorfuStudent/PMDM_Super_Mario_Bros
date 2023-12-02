using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private Vector2 velocity;
    private float inputAxis;

    // Configuración.
    public float speed = 8f;
    private float moveSpeed;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    // Estados del jugador.
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
    public bool falling => velocity.y < 0f && !grounded;

    /**
     * Al instanciarse el objeto que contiene el script obtiene los componentes necesarios.
     */
    private void Awake()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    /**
     * Esta función se ejecuta al habilitar el script.
     * Inicializa parte del personaje.
     */
    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }

    /**
     * Esta función se ejecuta al desabilitar el script.
     * Deshabilita parte del personaje.
     */
    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    /**
     * Esta función se ejecuta cada frame.
     * Controla el movimiento del jugador en su mayoría.
     */
    private void Update()
    {
        HorizontalMovement();

        grounded = rigidbody.Raycast(Vector2.down);

        if (grounded) {
            GroundedMovement();
        }

        ApplyGravity();
    }

    /**
     * Esta función se ejecuta manteniendo una lógica, para que sin importar de los frames, se ejecute siempre a la misma velocidad.
     */
    private void FixedUpdate()
    {
        // Mueve a mario dependiendo de su velocidad.
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        // Mantiene el personaje dentro de la camara.
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    /**
     * Se encarga de los movimientos horizontales del jugador.
     */
    private void HorizontalMovement()
    {
        // Acelera y desacelera.
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        // Verifica si estás corriendo contra una pared.
        if (rigidbody.Raycast(Vector2.right * velocity.x)) {
            velocity.x = 0f;
        }

        // Hace al sprite mirar hacia donde anda el personaje.
        if (velocity.x > 0f) {
            transform.eulerAngles = Vector3.zero;
        } else if (velocity.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        // Evita que la gravedad se acumule indefinidamente
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        // Hace saltar a Mario
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }

        if (Input.GetButton("Run"))
        {
            moveSpeed = speed * 1.5f;
        }
        else
        {
            moveSpeed = speed;
        }
    }

    /**
     * Aplica la gravedad al jugador.
     */
    private void ApplyGravity()
    {
        // Verifica si está callendo el jugador.
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        // Aplica la gravedad.
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    /**
     * Al colisionar con algo...
     * Si es un enemigo por arriba, el jugador rebota.
     * Si toca el techo con la cabeza, empieza a caer.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Rebota en la cabeza de un enemigo.
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            // Si choco con algo por encima dejo de subir.
            if (transform.DotTest(collision.transform, Vector2.up)) {
                velocity.y = 0f;
            }
        }
    }

}
