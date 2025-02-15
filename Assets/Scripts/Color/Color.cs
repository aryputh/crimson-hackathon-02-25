using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private string colorName;
    [SerializeField] private Color32 colorHex;

    public Color(PlayerStats playerStats, string colorName, Color32 colorHex)
    {
        this.playerStats = playerStats;
        this.colorName = colorName;
        this.colorHex = colorHex;
    }

    public Color32 GetColorHex()
    {
        return colorHex;
    }

    public PlayerStats GetColorStats()
    {
        return playerStats;
    }

    public string GetColorName()
    {
        return colorName;
    }
}
