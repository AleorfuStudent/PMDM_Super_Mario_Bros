using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{ 
    /**
     * Esta función se llama cuando el juego comienza.
     */
    private void Start()
    {
        // Añade una moneda al contador de monedas.
        GameManager.Instance.AddCoin();

        // Inicia la animación de la moneda.
        StartCoroutine(Animate());
    }

    /**
     * Esta función anima la moneda.
     */
    private IEnumerator Animate()
    {
        // Esta es la posición inicial de la moneda.
        Vector3 restingPosition = transform.localPosition;
        // Esta es la posición final de la monea.
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        // Mueve la moneda del punto inicial al final.
        yield return Move(restingPosition, animatedPosition);
        // Mueve la moneda del punto final al inicial.
        yield return Move(animatedPosition, restingPosition);

        // Destruye la moneda.
        Destroy(gameObject);
    }

    /**
     * Esta función mueve la moneda.
     */
    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        // Tiempo que ha transcurrido.
        float elapsed = 0f;
        // Tiempo que ha de durar.
        float duration = 0.25f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Se mueve de un punto a otro en un tiempo.
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }

}
