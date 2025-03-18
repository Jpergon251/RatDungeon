using UnityEngine;
using UnityEngine.UI;

public class StartDayButton : MonoBehaviour
{
    public TimeSystem timeSystem; // Asigna el TimeSystem desde el Inspector
    public Button button; // Asigna un botón UI desde el Inspector

    private void Start()
    {
        // Configurar el botón para iniciar el tiempo al hacer clic
        button.onClick.AddListener(StartDay);
    }

    private void StartDay()
    {
        timeSystem.StartTime();
        Debug.Log("El día ha comenzado.");
    }
}