using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private PlayerController playerController;

    public void SetActiveColor(int index)
    {
        playerController.ChangePlayerStats(colors[index].GetColorStats());
    }
}
