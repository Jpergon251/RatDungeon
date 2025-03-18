using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public TimeSystem timeSystem ; // Asigna el TimeSystem desde el Inspector
    public GameObject sun; // Asigna el Directional Light desde el Inspector
    public Gradient lightColorGradient; // Asigna un Gradient desde el Inspector

    private void Update()
    {
        if (timeSystem != null)
        {
            UpdateLighting(timeSystem.GetCurrentTime());
        }
    }

    private void UpdateLighting(float currentTime)
    {
        // Rotar el sol
        float sunRotation = Mathf.Lerp(-90, 270, currentTime / 24.0f); // -90° (amanecer) a 270° (anochecer)
        sun.transform.rotation = Quaternion.Euler(sunRotation, 0, 0);

        // Cambiar el color de la luz
        Color lightColor = lightColorGradient.Evaluate(currentTime / 24.0f);
        sun.GetComponent<Light>().color = lightColor;

        // Rotar el skybox
        float skyboxRotation = Mathf.Lerp(0, 360, currentTime / 24.0f);
        RenderSettings.skybox.SetFloat("_Rotation", skyboxRotation);
        DynamicGI.UpdateEnvironment(); // Actualizar la iluminación global
    }
}