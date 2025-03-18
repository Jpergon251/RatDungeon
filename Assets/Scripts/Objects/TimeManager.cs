using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float timeScale = 1.0f; // Escala de tiempo (1.0 = tiempo real)
    public float currentTime = 12.0f; // Hora actual (en formato 24 horas)
    public float dayLengthInMinutes = 10.0f; // Duración del día en minutos

    private float timeIncrement;

    private void Start()
    {
        // Calcular cuánto tiempo se debe incrementar por frame
        timeIncrement = 24.0f / (dayLengthInMinutes * 60.0f); // 24 horas divididas por la duración del día en segundos
    }

    private void Update()
    {
        // Incrementar el tiempo
        currentTime += timeIncrement * timeScale * Time.deltaTime;

        // Reiniciar el ciclo al llegar a 24 horas
        if (currentTime >= 24.0f)
        {
            currentTime -= 24.0f;
        }

        // Actualizar la iluminación y el skybox
        UpdateLighting();
    }

    private void UpdateLighting()
    {
        // Aquí ajustarás la iluminación y el skybox según la hora del día
        float sunRotation = Mathf.Lerp(-90, 270, currentTime / 24.0f); // Rotación del sol
        RenderSettings.skybox.SetFloat("_Rotation", sunRotation); // Rotar el skybox
        DynamicGI.UpdateEnvironment(); // Actualizar la iluminación global
    }
}