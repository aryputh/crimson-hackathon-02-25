using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Color : MonoBehaviour
{
    public string colorName = string.Empty;

    public Color(string colorName)
    {
        this.colorName = colorName;
    }
}
