using UnityEngine;

public static class Extensions
{
    // La capa principal que utilizar� este script.
    private static LayerMask layerMask = LayerMask.GetMask("Default");

    /**
     * Esta funci�n se encarga de generar un rayo que verifica la existencia de objetos en la direcci�n y distancia dadas.
     */
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic) {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        // Aqu� genera el rayo en forma de circulo, se encuentra inicialmente en la posici�n del jugador, con cierto radio, dirigiendose a cierta direcci�n,
        // a cierta distancia y solo respondiendo a cierta capa.
        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    /**
     * Esta funci�n es como el rayo, pero solo vigila una zona muy peque�a.
     */
    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }

}
