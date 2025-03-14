using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    public Transform terrain; // El terreno (objeto padre)
    public float orbitRadius = 50f; // Distancia del sol al centro del terreno
    public float dayDuration = 60f; // Tiempo en segundos para un ciclo completo

    private void Start()
    {
        if (terrain == null)
        {
            Debug.LogError("⚠️ Debes asignar el terreno en el inspector.");
            return;
        }

        // Posicionar la luz al inicio en la órbita
        transform.position = terrain.position + new Vector3(0f, orbitRadius, -orbitRadius);
        transform.LookAt(terrain); // Que mire al centro del terreno

        // Hacer que la luz sea hija del terreno
        transform.SetParent(terrain);
    }

    private void Update()
    {
        // Rotar la luz alrededor del terreno con el tiempo
        float rotationSpeed = 360f / dayDuration; // Grados por segundo
        transform.RotateAround(terrain.position, Vector3.right, rotationSpeed * Time.deltaTime);
    }
}