using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Renderizador de sprites
    private SpriteRenderer spriteRenderer;
    // Lista de objetos que pueden salir del boss.
    public GameObject[] items;
    // Número de vidas.
    public int maxHits = 20;

    public GameObject camera;
    public GameObject wall;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /**
     * Al colisionar con la cabeza del jugador, el bloque es golpeado.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (maxHits != 0 && obj.CompareTag("Player")) 
        { 
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                if (obj.GetComponent<PlayerMovement>().jumping) 
                {
                    Hit();
                }
                else
                {
                    obj.GetComponent<Player>().Hit();
                }
            }
            else
            {
                obj.GetComponent<Player>().Hit();
            }
        }
    }

    /**
     * Esta función contiene el golpe del bloque.
     */
    private void Hit()
    {
        maxHits--;
        // Inicia la Coroutine para cambiar el color después de un segundo
        StartCoroutine(Anim());

        if (maxHits == 0) 
        {
            GetComponent<DeathAnimation>().enabled = true;
            camera.GetComponent<SideScrolling>().enabled = true;
            Destroy(wall);
        }

        Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
    }

    // Hace una pequeña animación.
    private IEnumerator Anim()
    {
        // Cambia el color a rojo
        spriteRenderer.color = Color.red;

        // Espera un segundo
        yield return new WaitForSeconds(0.1f);

        // Cambia el color a blanco
        spriteRenderer.color = Color.white;
    }
}
