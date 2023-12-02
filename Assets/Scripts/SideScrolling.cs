using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrolling : MonoBehaviour
{
    // La camara y el jugador.
    private new Camera camera;
    private Transform player;

    // La altura de la camara, tanto en la superficie como bajo tierra.
    public float height = 6.5f;
    public float undergroundHeight = -9.5f;
    public float undergroundThreshold = 0f;

    /**
     * Al insanciarse el objeto que contiene este script.
     */
    private void Awake()
    {
        // Obtengo los componentes necesarios.
        camera = GetComponent<Camera>();
        player = GameObject.FindWithTag("Player").transform;
    }

    /**
     * Como última actualización en el frame.
     */
    private void LateUpdate()
    {
        // La camara persigue al jugador hacia la derecha.
        Vector3 cameraPosition = transform.position;
        // Evitando de la siguiente manera volver hacía atrás.
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }

    /**
     * Indico si el jugador se encuentra o no bajo tierra.
     */
    public void SetUnderground(bool underground)
    {
        // Bajo la camara y la coloco en el lugar necesario.
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }

}
