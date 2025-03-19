using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    public Light environmentLight; // Referencia a la luz ambiental (Directional Light)
    public float rotationSpeed = 10f; // Velocidad de rotación en grados por segundo

    private void Update()
    {
        // Rotar la luz ambiental en el eje X
        if (environmentLight != null)
        {
            float angle = rotationSpeed * Time.deltaTime;
            environmentLight.transform.Rotate(angle, 0, 0);
        }
        else
        {
            Debug.LogWarning("No se ha asignado una luz ambiental.");
        }
    }
}