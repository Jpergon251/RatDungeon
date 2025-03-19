using System.Collections.Generic;
using Player;
using UnityEngine;

public class CheeseBehavior : MonoBehaviour
{
    public enum Rarity { Common, Rare, Epic, Legendary } // Rarezas de los quesos
    public Rarity rarity; // Rareza de este queso

    private float lifetime = 5f; // Tiempo de vida del queso en segundos

    
    // Lista de probabilidades para cada rareza
    private List<(Rarity rarity, int weight)> rarityProbabilities = new List<(Rarity, int)>
    {
        (Rarity.Common, 50),    // 50% de probabilidad
        (Rarity.Rare, 30),      // 30% de probabilidad
        (Rarity.Epic, 15),      // 15% de probabilidad
        (Rarity.Legendary, 5)   // 5% de probabilidad
    };
    private void Start()
    {
        // Asignar una rareza aleatoria al queso
        AssignRandomRarity();

        // Configurar el color del contorno según la rareza
        SetOutlineColor();

        // Destruir el queso después de 5 segundos si no es recolectado
        Destroy(gameObject, lifetime);
    }

    void AssignRandomRarity()
    {
        // Calcular el total de pesos
        int totalWeight = 0;
        foreach (var prob in rarityProbabilities)
        {
            totalWeight += prob.weight;
        }

        // Generar un número aleatorio dentro del rango de pesos
        int randomValue = Random.Range(0, totalWeight);

        // Seleccionar la rareza basada en el valor aleatorio
        int cumulativeWeight = 0;
        foreach (var prob in rarityProbabilities)
        {
            cumulativeWeight += prob.weight;
            if (randomValue < cumulativeWeight)
            {
                rarity = prob.rarity;
                break;
            }
        }
    }

    void SetOutlineColor()
    {
        Color outlineColor = Color.green; // Por defecto, común

        switch (rarity)
        {
            case Rarity.Common:
                outlineColor = Color.green;
                break;
            case Rarity.Rare:
                outlineColor = Color.blue;
                break;
            case Rarity.Epic:
                outlineColor = new Color(0.5f, 0f, 0.5f); // Morado
                break;
            case Rarity.Legendary:
                outlineColor = new Color(1f, 0.84f, 0f); // Dorado
                break;
        }

        // Obtener el MeshRenderer
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        // Buscar el material llamado "OutlineShader"
        foreach (Material material in meshRenderer.materials)
        {
            if (material.name == "OutlineShader" || material.name.StartsWith("OutlineShader"))
            {
                // Cambiar el color del contorno
                material.SetColor("_OutlineColor", outlineColor);
                break; // Salir del bucle una vez encontrado
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Verificar si el jugador colisiona con el queso
        if (other.body.CompareTag("Player"))
        {
            // Obtener el componente PlayerController del jugador
            PlayerController player = other.body.GetComponent<PlayerController>();
            if (player != null)
            {
                // Otorgar XP al jugador según la rareza
                player.AddXP(GetXPAmount());

                // Destruir el queso
                Destroy(gameObject);
            }
        }
    }

    int GetXPAmount()
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 10;
            case Rarity.Rare:
                return 50;
            case Rarity.Epic:
                return 100;
            case Rarity.Legendary:
                return 250;
            default:
                return 0;
        }
    }
}