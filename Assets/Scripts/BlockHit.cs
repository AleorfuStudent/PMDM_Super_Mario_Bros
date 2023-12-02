using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    // Aquí se guarda el objeto que va a salir del bloque
    public GameObject item;
    // Este es el sprite del bloque vacío
    public Sprite emptyBlock;
    public int maxHits = -1;
    private bool animating;

    /**
     * Al colisionar con la cabeza del jugador, el bloque es golpeado.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up)) {
                Hit();
            }
        }
    }

    /**
     * Esta función contiene el golpe del bloque.
     */
    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        maxHits--;

        if (maxHits == 0) {
            // Si ya ha sido golpeado todas las veces posibles, cambia su sprite al de un bloque vacío.
            spriteRenderer.sprite = emptyBlock;
        }

        // Si contiene un objeto, instanciarlo.
        if (item != null) {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        // Anima el bloque.
        StartCoroutine(Animate());
    }

    /**
     * Esta función anima el movimiento del golpe.
     */
    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }

    /**
     * Esta función mueve el bloque para hacer la animación.
     */
    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }

}
