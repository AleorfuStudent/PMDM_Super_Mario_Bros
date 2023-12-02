using UnityEngine;

public class PowerUp : MonoBehaviour
{

    // Lista de tipos de objetos.
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    // Aquí se guarda uno de los tipos.
    public Type type;

    /**
     * Al ser tocado, si es un jugador, el jugador obtiene el objeto.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Collect(other.gameObject);
        }
    }

    /**
     * Al obtener el objeto, dependiendo del tipo, ocurre algo.
     */
    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                // De ser una moneda, sumo una moneda.
                GameManager.Instance.AddCoin();
                break;

            case Type.ExtraLife:
                // De ser una vida, sumo una vida.
                GameManager.Instance.AddLife();
                break;

            case Type.MagicMushroom:
                // De ser una seta, el personaje crece.
                player.GetComponent<Player>().Grow();
                break;

            case Type.Starpower:
                // De ser una estrella, el personaje adquiere invencibilidad temporal.
                player.GetComponent<Player>().Starpower();
                break;
        }

        // Una vez adquirido, el objeto se borra.
        Destroy(gameObject);
    }

}
