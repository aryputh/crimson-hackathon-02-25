using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushColor : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private string colorName;
    [SerializeField] private Color color;

    public string Name => name;
    public Color Color => color;
    public PlayerStats Stats => playerStats;
}
