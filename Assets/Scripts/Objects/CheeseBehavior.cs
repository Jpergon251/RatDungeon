using Player;
using UnityEngine;

public class CheeseBehavior : MonoBehaviour
{
    public enum Rarity { Common, Rare, Epic, Legendary } // Rarezas de los quesos
    public Rarity rarity; // Rareza de este queso

    public Material outlineShader; // Material del queso

    private float lifetime = 5f; // Tiempo de vida del queso en segundos

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
        // Asignar una rareza aleatoria
        rarity = (Rarity)Random.Range(0, System.Enum.GetValues(typeof(Rarity)).Length);
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

        // Aplicar el color al material
        outlineShader.SetColor("_OutlineColor", outlineColor);
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