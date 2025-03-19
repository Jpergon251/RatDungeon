using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    [Header("Light Settings")]
    public Light directionalLight; // La luz direccional que simulará el sol
    public float dayDuration = 120f; // Duración de un ciclo día-noche en segundos
    public Gradient lightColorGradient; // Gradiente para cambiar el color de la luz
    public AnimationCurve lightIntensityCurve; // Curva para ajustar la intensidad de la luz

    private float rotationSpeed; // Velocidad de rotación calculada

    private void Start()
    {
        if (directionalLight == null)
        {
            Debug.LogError("Asigna una luz direccional en el Inspector.");
            return;
        }

        // Calcular la velocidad de rotación basada en la duración del día
        rotationSpeed = 360f / dayDuration;
    }

    private void Update()
    {
        if (directionalLight == null) return;

        // Rotar la luz alrededor del eje X
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        // Calcular el progreso del ciclo día-noche (0 a 1)
        float progress = Mathf.Repeat(transform.eulerAngles.x / 360f, 1f);

        // Cambiar el color de la luz basado en el gradiente
        directionalLight.color = lightColorGradient.Evaluate(progress);

        // Cambiar la intensidad de la luz basado en la curva
        directionalLight.intensity = lightIntensityCurve.Evaluate(progress);
    }
}