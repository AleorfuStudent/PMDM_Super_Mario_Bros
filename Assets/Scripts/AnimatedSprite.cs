using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    // Array de sprites que se van a mostrar.
    public Sprite[] sprites;
    // Velocidad de la animaci�n en frames por segundo.
    public float framerate = 1f / 6f;

    // Referencia al componente SpriteRenderer del objeto.
    private SpriteRenderer spriteRenderer;
    // Frame actual de la animaci�n.
    private int frame;

    /**
     * Esta funci�n se llama cuando se crea el objeto.
     */
    private void Awake()
    {
        // Obtiene el componente SpriteRenderer del objeto.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /**
     * Esta funci�n se llama cuando se activa el objeto.
     */
    private void OnEnable()
    {
        // Llama a la funci�n Animate cada framerate segundos.
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    /**
     * Esta funci�n se llama cuando se desactiva el objeto.
     */
    private void OnDisable()
    {
        // Cancela la llamada a la funci�n Animate.
        CancelInvoke();
    }

    /**
     * Esta funci�n se llama cada framerate segundos.
     */
    private void Animate()
    {
        // Incrementa el frame actual.
        frame++;

        // Si el frame actual es mayor o igual que el n�mero de sprites,
        if (frame >= sprites.Length) {
            // Reinicia el frame actual.
            frame = 0;
        }

        // Si el frame actual est� dentro del rango de sprites,
        if (frame >= 0 && frame < sprites.Length) {
            // Cambia el sprite del SpriteRenderer al sprite actual.
            spriteRenderer.sprite = sprites[frame];
        }
    }

}
