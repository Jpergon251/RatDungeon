using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public GameObject cheesePrefab; // Prefab del queso
    public float launchForce = 5f; // Fuerza con la que se lanza el queso

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador (rata) colisiona con el cofre
        if (other.CompareTag("Player"))
        {
            // Lanzar el queso
            LaunchCheese();

            // Destruir el cofre
            Destroy(gameObject);
        }
    }

    void LaunchCheese()
    {
        if (cheesePrefab != null)
        {
            // Instanciar el queso en la posición del cofre
            GameObject cheese = Instantiate(cheesePrefab, transform.position, Quaternion.identity);

            // Aplicar una fuerza al queso para lanzarlo
            Rigidbody cheeseRigidbody = cheese.GetComponent<Rigidbody>();
            if (cheeseRigidbody != null)
            {
                Vector3 launchDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
                cheeseRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("No se ha asignado un prefab de queso.");
        }
    }
}