using UnityEngine;

public class Goomba : MonoBehaviour
{
    // Sprite del Goomba aplastado.
    public Sprite flatSprite;

    /**
     * Al ser pisado por el jugador aplastarse.
     * Al ser tocado por el jugador con la estrella de poder, recibir daño.
     * Al ser tocado por el jugador de forma corriente, dañar al jugador.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
            } else {
                player.Hit();
            }
        }
    }

    /**
     * Al tocal a una concha en movimiento, morir.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }

    /**
     * Esta función aplasta al goomba, y a los 0.5 segundos, el objeto se destuirá.
     */
    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    /**
     * Al ser golpeado, ejecutar la animación de muerte y destruir a los 3 segundos.
     */
    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

}
