using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    // Indico el rendecizador de sprites y el sprite de muerte.
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    /**
     * Esta función se ejecuta cuando se resetea los valores de un componente desde la interfaz de Unity.
     */
    private void Reset()
    {
        // Obtiene el componente del renderizador de sprites.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /**
     * Esta función se ejecuta al habilitar el script.
     */
    private void OnEnable()
    {
        // Ejecuta diversas funciones para animar la muerte del jugador.
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    /**
     * Esta función se ejecuta al deshabiitar el script.
     */
    private void OnDisable()
    {
        // Para todas las corutinas, que no son más que funciones ejecutandose indefinidamente.
        StopAllCoroutines();
    }

    /**
     * Esta función cambia el sprite del jugador por el de muerto.
     */
    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;

        if (deadSprite != null) {
            spriteRenderer.sprite = deadSprite;
        }
    }

    /**
     * Esta función deshabilita las físicas del objeto.
     */
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().isKinematic = true;

        BossBehaviour bossBehaviour = GetComponent<BossBehaviour>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        // Desactiva el script del comportamiento del boss.
        if (bossBehaviour != null ) 
        {
            bossBehaviour.enabled = false;        
        }
        // Desactiva el script de movimento del jugador.
        if (playerMovement != null) {
            playerMovement.enabled = false;
        }
        // Desactiva el script de movimiento de las entidades.
        if (entityMovement != null) {
            entityMovement.enabled = false;
        }
    }

    /**
     * Esta función anima al objeto para que suba y baje.
     */
    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
