using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Configuraciones.
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;

    /**
     * Si mientras el jugador se encuentra junto a la tubería pulsa la tecla indicada.
     */
    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (Input.GetKey(enterKeyCode)) {
                // Comienza la animación.
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    /**
     * Animación de entrar en la tubería.
     */
    private IEnumerator Enter(Transform player)
    {
        // Evita el movimiento del jugador.
        player.GetComponent<PlayerMovement>().enabled = false;

        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);

        // Mueve la camara a la posición final de la tubería.
        var sideSrolling = Camera.main.GetComponent<SideScrolling>();
        sideSrolling.SetUnderground(connection.position.y < sideSrolling.undergroundThreshold);

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }

        player.GetComponent<PlayerMovement>().enabled = true;
    }

    /**
     * Mueve al jugador de un punto a otro.
     */
    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;
    }

}
