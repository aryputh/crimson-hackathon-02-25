using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private List<BrushColor> colors = new List<BrushColor>();
    [SerializeField] private int currentColorIndex = 0;

    public BrushColor GetCurrentColor()
    {
        if (currentColorIndex >= 0 && currentColorIndex < colors.Count)
        {
            return colors[currentColorIndex];
        }
        return null;
    }

    public void SetCurrentColor(int index)
    {
        if (index >= 0 && index < colors.Count)
        {
            currentColorIndex = index;
        }
    }

    public PlayerStats GetStatsFromColor(Color groundColor)
    {
        foreach (var colorData in colors)
        {
            if (colorData.Color == groundColor)
            {
                return colorData.Stats;
            }
        }

        return new PlayerStats();
    }
}
