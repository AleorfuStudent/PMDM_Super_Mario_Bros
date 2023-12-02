using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{ 
    /**
     * Esta funci�n se llama cuando el juego comienza.
     */
    private void Start()
    {
        // A�ade una moneda al contador de monedas.
        GameManager.Instance.AddCoin();

        // Inicia la animaci�n de la moneda.
        StartCoroutine(Animate());
    }

    /**
     * Esta funci�n anima la moneda.
     */
    private IEnumerator Animate()
    {
        // Esta es la posici�n inicial de la moneda.
        Vector3 restingPosition = transform.localPosition;
        // Esta es la posici�n final de la monea.
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        // Mueve la moneda del punto inicial al final.
        yield return Move(restingPosition, animatedPosition);
        // Mueve la moneda del punto final al inicial.
        yield return Move(animatedPosition, restingPosition);

        // Destruye la moneda.
        Destroy(gameObject);
    }

    /**
     * Esta funci�n mueve la moneda.
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
