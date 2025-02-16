using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private List<BrushColor> brushColors = new List<BrushColor>();
    [SerializeField] private int currentColorIndex = 0;

    public BrushColor GetCurrentColor()
    {
        if (currentColorIndex >= 0 && currentColorIndex < brushColors.Count)
        {
            return brushColors[currentColorIndex];
        }
        return null;
    }

    public void SetCurrentColor(int index)
    {
        if (index >= 0 && index < brushColors.Count)
        {
            currentColorIndex = index;
        }
    }

    public PlayerStats GetStatsFromColor(Color color)
    {
        string detectedHex = ColorUtility.ToHtmlStringRGB(color);  // Convert detected color to Hex

        foreach (BrushColor brushColor in brushColors)
        {
            string brushHex = ColorUtility.ToHtmlStringRGB(brushColor.GetColor());  // Convert stored color to Hex

            if (brushHex == detectedHex)  // Compare as strings
            {
                Debug.Log($"Found BrushColor for {detectedHex}. Returning PlayerStats reference.");
                return brushColor.GetPlayerStats();
            }
        }

        Debug.LogWarning($"No matching BrushColor found for {detectedHex}! Returning default stats.");
        return ScriptableObject.CreateInstance<PlayerStats>();
    }
}
