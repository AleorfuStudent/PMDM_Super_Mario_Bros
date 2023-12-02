using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    // Componentes de posición, rotación y escalado
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    // Configuración
    public float speed = 6f;
    public int nextWorld = 1;
    public int nextStage = 1;

    /**
     * Si el jugador toca la bandera, hace la animación de bajar y después de completar el nivel.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(other.transform));
        }
    }

    /**
     * Mueve el jugador hacia el castillo y pasa de nivel.
     */
    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        yield return MoveTo(player, poleBottom.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        GameManager.Instance.LoadLevel(nextWorld, nextStage);
    }

    /**
     * Hace el desplazamiento de un objeto a una posición.
     */
    private IEnumerator MoveTo(Transform subject, Vector3 position)
    {
        while (Vector3.Distance(subject.position, position) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, position, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = position;
    }

}
