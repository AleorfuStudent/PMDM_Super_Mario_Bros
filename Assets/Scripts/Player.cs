using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Aqu� indico los renderizaremos que utilizaremos.
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    // Aqu� se guarda el que estemos utilizando.
    private PlayerSpriteRenderer activeRenderer;

    // Caja de colisiones del jugador.
    public CapsuleCollider2D capsuleCollider { get; private set; }
    // Script de animaci�n de muerte.
    public DeathAnimation deathAnimation { get; private set; }

    // Variables que indican el estado del jugador.
    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

    /**
     * Esta funci�n se ejecuta al instanciarse el objeto que contiene este script.
     */
    private void Awake()
    {
        // Se obtienen los componentes necesarios.
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathAnimation = GetComponent<DeathAnimation>();
        // Indico el renderizador que se est� utilizando en el momento.
        activeRenderer = smallRenderer;
    }

    /**
     * Esta funci�n hace que el jugador sufra un da�o.
     */
    public void Hit()
    {
        // Mientras el jugador no est� muerto o con una estrella de poder.
        if (!dead && !starpower)
        {
            // Si est� grande, encoger. Si no est� grande, morir.
            if (big) {
                Shrink();
            } else {
                Death();
            }
        }
    }

    /**
     * Esta funci�n hace que el jugador muera.
     */
    public void Death()
    {
        // Desabilita algunos componentes y actica el script que anima la muerte.
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        // Llama a la funci�n para que reinicie el nivel en tres segundos.
        GameManager.Instance.ResetLevel(3f);
    }

    /**
     * Esta funci�n hace crecer al jugador.
     */
    public void Grow()
    {
        // Modifica los componentes necesarios para cumplir su funci�n.
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        // Cambia la forma de la caja de colisiones.
        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        // Activa la animaci�n de crecer.
        StartCoroutine(ScaleAnimation());
    }

    /**
     * Esta funci�n hace encoger al jugador.
     */
    public void Shrink()
    {
        // Modifica los componentes necesarios para cumplir su funci�n.
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        // Cambia la forma de la caja de colisiones.
        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        // Activa la funci�n para encoger.
        StartCoroutine(ScaleAnimation());
    }

    /**
     * Esta funci�n anima los cambios de tama�o.
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

        // Cambia de tama�o.
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    /**
     * Esta funci�n activa la funci�n que anima el estado de invencibilidad.
     */
    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    /**
     * Esta funci�n anima el estado de invencibilidad.
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
