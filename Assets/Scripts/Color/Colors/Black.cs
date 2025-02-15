using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : Color
{
    [SerializeField] private string colorName;
    [SerializeField] private Color color;
    [SerializeField] private PlayerStats playerStats;

    public Black(string colorName, Color color) : base(colorName, color)
    {
        this.colorName = colorName;
        this.color = color;
    }

    public override PlayerStats GetColorStats()
    {
        return playerStats;
    }
}
