using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    // Array de sprites que se van a mostrar.
    public Sprite[] sprites;
    // Velocidad de la animación en frames por segundo.
    public float framerate = 1f / 6f;

    // Referencia al componente SpriteRenderer del objeto.
    private SpriteRenderer spriteRenderer;
    // Frame actual de la animación.
    private int frame;

    /**
     * Esta función se llama cuando se crea el objeto.
     */
    private void Awake()
    {
        // Obtiene el componente SpriteRenderer del objeto.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /**
     * Esta función se llama cuando se activa el objeto.
     */
    private void OnEnable()
    {
        // Llama a la función Animate cada framerate segundos.
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    /**
     * Esta función se llama cuando se desactiva el objeto.
     */
    private void OnDisable()
    {
        // Cancela la llamada a la función Animate.
        CancelInvoke();
    }

    /**
     * Esta función se llama cada framerate segundos.
     */
    private void Animate()
    {
        // Incrementa el frame actual.
        frame++;

        // Si el frame actual es mayor o igual que el número de sprites,
        if (frame >= sprites.Length) {
            // Reinicia el frame actual.
            frame = 0;
        }

        // Si el frame actual está dentro del rango de sprites,
        if (frame >= 0 && frame < sprites.Length) {
            // Cambia el sprite del SpriteRenderer al sprite actual.
            spriteRenderer.sprite = sprites[frame];
        }
    }

}
