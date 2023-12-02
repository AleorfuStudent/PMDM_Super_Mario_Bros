using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Aquí indico los renderizaremos que utilizaremos.
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    // Aquí se guarda el que estemos utilizando.
    private PlayerSpriteRenderer activeRenderer;

    // Caja de colisiones del jugador.
    public CapsuleCollider2D capsuleCollider { get; private set; }
    // Script de animación de muerte.
    public DeathAnimation deathAnimation { get; private set; }

    // Variables que indican el estado del jugador.
    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

    /**
     * Esta función se ejecuta al instanciarse el objeto que contiene este script.
     */
    private void Awake()
    {
        // Se obtienen los componentes necesarios.
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathAnimation = GetComponent<DeathAnimation>();
        // Indico el renderizador que se está utilizando en el momento.
        activeRenderer = smallRenderer;
    }

    /**
     * Esta función hace que el jugador sufra un daño.
     */
    public void Hit()
    {
        // Mientras el jugador no esté muerto o con una estrella de poder.
        if (!dead && !starpower)
        {
            // Si está grande, encoger. Si no está grande, morir.
            if (big) {
                Shrink();
            } else {
                Death();
            }
        }
    }

    /**
     * Esta función hace que el jugador muera.
     */
    public void Death()
    {
        // Desabilita algunos componentes y actica el script que anima la muerte.
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        // Llama a la función para que reinicie el nivel en tres segundos.
        GameManager.Instance.ResetLevel(3f);
    }

    /**
     * Esta función hace crecer al jugador.
     */
    public void Grow()
    {
        // Modifica los componentes necesarios para cumplir su función.
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        // Cambia la forma de la caja de colisiones.
        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        // Activa la animación de crecer.
        StartCoroutine(ScaleAnimation());
    }

    /**
     * Esta función hace encoger al jugador.
     */
    public void Shrink()
    {
        // Modifica los componentes necesarios para cumplir su función.
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        // Cambia la forma de la caja de colisiones.
        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        // Activa la función para encoger.
        StartCoroutine(ScaleAnimation());
    }

    /**
     * Esta función anima los cambios de tamaño.
     */
    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Hace parpadear el sprite en ciertos frames.
            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        // Cambia de tamaño.
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    /**
     * Esta función activa la función que anima el estado de invencibilidad.
     */
    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    /**
     * Esta función anima el estado de invencibilidad.
     */
    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Cambia de color cada ciertos frames.
            if (Time.frameCount % 4 == 0) {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }

}
