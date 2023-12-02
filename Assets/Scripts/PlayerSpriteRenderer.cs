using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteRenderer : MonoBehaviour
{
    // Script encargado del movimiento del jugador.
    private PlayerMovement movement;

    // Componente encargado de cargar el sprite.
    public SpriteRenderer spriteRenderer { get; private set; }

    // Distintos sprites que se utilizar�n.
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;

    // Script encargado de la animaci�n de correr del jugador.
    public AnimatedSprite run;

    /**
     * Esta funci�n se ejecuta al instanciar el objeto que contiene este script.
     */
    private void Awake()
    {
        // Obtengo los componentes necesarios.
        movement = GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Esta funci�n se ejecuta al final del frame.
    private void LateUpdate()
    {
        // La habilitaci�n del script de animar al jugador corriendo se condiciona de si el jugador se encuentra corriendo o no.
        run.enabled = movement.running;

        // Dependiendo de que est� haciendo el jugador, se coloca un sprite u otro.
        if (movement.jumping) {
            spriteRenderer.sprite = jump;
        } else if (movement.sliding) {
            spriteRenderer.sprite = slide;
        } else if (!movement.running) {
            spriteRenderer.sprite = idle;
        }
    }

    /**
     * Al habilitar este script se activa el renderizador de sprites.
     */
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    /**
     * Al deabilitar este script se desabilita el renderizador de sprites y el script de la animaci�n de correr.
     */
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

}
