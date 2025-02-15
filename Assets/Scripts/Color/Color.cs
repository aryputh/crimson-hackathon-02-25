using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Color : MonoBehaviour
{
    private string colorName;
    private Color color;

    public Color(string colorName, Color color)
    {
        this.colorName = colorName;
        this.color = color;
    }

    public abstract PlayerStats GetColorStats();
}
