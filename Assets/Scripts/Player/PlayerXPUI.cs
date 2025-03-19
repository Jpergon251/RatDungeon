using Player;
using TMPro;
using UnityEngine;

public class PlayerXPUI : MonoBehaviour
{
    public PlayerController player;
    public TextMeshPro xpText;
    public TextMeshPro levelText;

  

    void Update()
    {
        if (player != null)
        {
            xpText.text = $"XP: {player.currentXP} / {player.xpToNextLevel}";
            levelText.text = $"Nivel: {player.currentLevel}";
        }
    }
}