using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [Header("Time Settings")]
    public float timeScale = 1.0f; // Escala de tiempo (1.0 = tiempo real)
    public float startTime = 6.0f; // Hora de inicio (en formato 24 horas)
    public float dayLengthInMinutes = 10.0f; // Duración del día en minutos

    private float currentTime;
    private float timeIncrement;
    private bool isTimeRunning = false;

    private void Start()
    {
        // Calcular cuánto tiempo se debe incrementar por frame
        timeIncrement = 24.0f / (dayLengthInMinutes * 60.0f); // 24 horas divididas por la duración del día en segundos
    }

    private void Update()
    {
        if (isTimeRunning)
        {
            // Incrementar el tiempo
            currentTime += timeIncrement * timeScale * Time.deltaTime;

            // Reiniciar el ciclo al llegar a 24 horas
            if (currentTime >= 24.0f)
            {
                currentTime -= 24.0f;
            }

            // Llamar a eventos relacionados con el tiempo (opcional)
            OnTimeUpdated();
        }
    }

    public void StartTime()
    {
        currentTime = startTime; // Iniciar en la hora especificada
        isTimeRunning = true;
    }

    public void StopTime()
    {
        isTimeRunning = false;
    }

    public void ResetTime()
    {
        currentTime = startTime;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    private void OnTimeUpdated()
    {
        // Aquí puedes notificar a otros sistemas que el tiempo ha cambiado
        Debug.Log($"Hora actual: {currentTime:00.00}");
    }
}