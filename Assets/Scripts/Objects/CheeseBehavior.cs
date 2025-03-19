using Player;
using UnityEngine;

public class CheeseBehavior : MonoBehaviour
{
    public enum Rarity { Common, Rare, Epic, Legendary } // Rarezas de los quesos
    public Rarity rarity; // Rareza de este queso

    public Material cheeseMaterial; // Material del queso

    private void Start()
    {
        // Configurar el color del contorno según la rareza
        SetOutlineColor();
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
        cheeseMaterial.SetColor("_OutlineColor", outlineColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador colisiona con el queso
        if (other.CompareTag("Player"))
        {
            // Obtener el componente PlayerController del jugador
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // Otorgar XP al jugador
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