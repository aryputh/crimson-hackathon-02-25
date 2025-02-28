using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorChanger : MonoBehaviour
{
    [Header("Color Settings")]
    [SerializeField] private List<Color> colors = new List<Color>(); // List of colors
    [SerializeField] private float changeSpeed = 2f; // Time (in seconds) per transition

    private SpriteRenderer spriteRenderer;
    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float t = 0f; // Time progress between colors

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (colors.Count > 0)
        {
            spriteRenderer.color = colors[0]; // Set initial color
        }
    }

    void Update()
    {
        if (colors.Count < 2) return; // Need at least 2 colors to cycle

        // Smoothly transition between current and next color
        t += Time.deltaTime / changeSpeed; // Normalize transition over time

        spriteRenderer.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);

        if (t >= 1f)
        {
            t = 0f; // Reset progress
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % colors.Count; // Cycle to next color
        }
    }
}
