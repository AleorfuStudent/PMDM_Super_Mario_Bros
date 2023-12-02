using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    /**
     * Al empezar el juego inicia la animaci�n.
     */
    private void Start()
    {
        StartCoroutine(Animate());
    }

    /**
     * Animaci�n de los objetos
     */
    private IEnumerator Animate()
    {
        // Obtiene los componentes esenciales.
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Configura el objeto para prepararlo para la animaci�n.
        rigidbody.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        // Muestra el objeto.
        spriteRenderer.enabled = true;

        // Ejecuta la animaci�n de salir del bloque.
        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.up;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // Una vez terminada la animaci�n deja normal el objeto para que pueda moverse.
        rigidbody.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }

}
