using UnityEngine;

public class CheeseBehavior : MonoBehaviour
{
    public float lifetime = 5f; // Tiempo antes de que el queso desaparezca

    void Start()
    {
        // Destruir el queso después de un tiempo
        Destroy(gameObject, lifetime);
    }
}
