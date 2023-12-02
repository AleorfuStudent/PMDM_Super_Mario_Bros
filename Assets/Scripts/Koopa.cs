using UnityEngine;

public class Koopa : MonoBehaviour
{
    // Sprite del cascarón y su velocidad.
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    // Estados del Koopa.
    private bool shelled;
    private bool pushed;

    /**
     * Al colisionar con el jugador...
     * Si el jugador tiene una estrella de poder, morir instantaneamente.
     * Si el jugador lo pisa, convertirse en cascarón.
     * Si no, sañar al jugador.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                EnterShell();
            }  else {
                player.Hit();
            }
        }
    }

    /**
     * Al tocar al jugador cuando el Koopa se encuentra en forma de concha, salir disparado.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si toca a un jugador, empujarlo o recibir daño.
        // Si toca otra concha, recibir daño.
        if (shelled && other.CompareTag("Player"))
        {
            // Si la concha no se mueve, lanzarla.
            // Si la concha se mueve, recibir daño si no se tiene la estrella de poder.
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.starpower) {
                    Hit();
                } else {
                    player.Hit();
                }
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    /**
     * Entra en forma de concha.
     */
    private void EnterShell()
    {
        shelled = true;

        GetComponent<SpriteRenderer>().sprite = shellSprite;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
    }

    /**
     * Esta función le añade velocidad a la concha.
     */
    private void PushShell(Vector2 direction)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    /**
     * Activar la animación de muerte y destruirse a los tres segundos.
     */
    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    /**
     * Cuando el caparazón se sale de la pantalla, se destruye.
     */
    private void OnBecameInvisible()
    {
        if (pushed) {
            Destroy(gameObject);
        }
    }

}
