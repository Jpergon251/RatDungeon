using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material skyboxMaterial; // Material del Skybox
    public float dayDuration = 60f; // Duración de un ciclo día/noche en segundos
    
    public Light environmentLight;

    private void Update()
    {
        // Calcular el tiempo total normalizado (0 a 1)
        float normalizedTime = Mathf.Repeat(Time.time / dayDuration, 3f);

        // Calcular los tiempos para cada fase
        float time1 = Mathf.Clamp01(normalizedTime * 3f);       // Día a tarde (0 a 1)
        float time2 = Mathf.Clamp01((normalizedTime - 1f) * 3f); // Tarde a noche (1 a 2)
        float time3 = Mathf.Clamp01((normalizedTime - 2f) * 3f); // Noche a día (2 a 3)

        // Pasar los tiempos al material del Skybox
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetFloat("_CustomTime1", time1);
            skyboxMaterial.SetFloat("_CustomTime2", time2);
            skyboxMaterial.SetFloat("_CustomTime3", time3);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un material de Skybox.");
        }

        // Rotar la luz en función del ciclo de día/noche
        /*if (environmentLight != null)
        {
            float sunElevation = Mathf.Lerp(0f, 360f, normalizedTime); // Movimiento en X (día/noche)
            // float sunOrbit = Mathf.Sin(normalizedTime * Mathf.PI * 2f) * 15f; // Inclinación orbital en Y
            float sunOrbit = Mathf.Lerp(0f, 180f, normalizedTime);
            environmentLight.transform.rotation = Quaternion.Euler(sunElevation , 90f + sunOrbit, 0f);
        }*/
    }
}