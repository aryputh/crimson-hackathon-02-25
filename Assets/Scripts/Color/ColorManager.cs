using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Color currentColor;

    public Color GetCurrentColor()
    {
        return currentColor;
    }

    public void SetActiveColor(int index)
    {
        currentColor = colors[index];
        playerController.ChangePlayerStats(currentColor.GetColorStats());
    }
}
