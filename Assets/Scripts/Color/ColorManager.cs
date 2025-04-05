using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorDetails
{
    public bool isUsable;
    public BrushColor brushColor;
    public GameObject paletteUiSelection;
}

public class ColorManager : MonoBehaviour
{
    [SerializeField] private List<ColorDetails> colorDetails = new List<ColorDetails>();
    [SerializeField] private int currentColorIndex = 0;

    [Header("Animation Settings")]
    [SerializeField] private float selectedScale = 1.1f;
    [SerializeField] private float normalScale = 1.0f;
    [SerializeField] private float pulseSpeed = 2.0f;

    private Coroutine activePulseCoroutine;

    private void Start()
    {
        for (int i = 0; i < colorDetails.Count; i++)
        {
            bool isActive = colorDetails[i].isUsable;
            colorDetails[i].paletteUiSelection.SetActive(isActive);
            colorDetails[i].paletteUiSelection.transform.localScale = Vector3.one * normalScale;
        }

        if (colorDetails.Count > 0)
        {
            var current = colorDetails[currentColorIndex];
            current.paletteUiSelection.SetActive(true);
            activePulseCoroutine = StartCoroutine(PulseSelectionScale(current.paletteUiSelection.transform));
        }
    }

    public BrushColor GetCurrentColor()
    {
        if (currentColorIndex >= 0 && currentColorIndex < colorDetails.Count)
            return colorDetails[currentColorIndex].brushColor;

        return null;
    }

    public void SetCurrentColor(int index)
    {
        if (index < 0 || index >= colorDetails.Count) return;

        for (int i = 0; i < colorDetails.Count; i++)
        {
            colorDetails[i].paletteUiSelection.transform.localScale = Vector3.one * normalScale;
        }

        currentColorIndex = index;
        GameObject selected = colorDetails[currentColorIndex].paletteUiSelection;
        selected.SetActive(true);

        if (activePulseCoroutine != null)
            StopCoroutine(activePulseCoroutine);

        activePulseCoroutine = StartCoroutine(PulseSelectionScale(selected.transform));
    }

    private IEnumerator PulseSelectionScale(Transform target)
    {
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime * pulseSpeed * Mathf.PI;
            float scale = Mathf.Lerp(normalScale, selectedScale, (Mathf.Sin(t) + 1f) / 2f);
            target.localScale = Vector3.one * scale;
            yield return null;
        }
    }

    public PlayerStats GetStatsFromColor(Color color)
    {
        string detectedHex = ColorUtility.ToHtmlStringRGB(color);

        for (int i = 0; i < colorDetails.Count; i++)
        {
            string brushHex = ColorUtility.ToHtmlStringRGB(colorDetails[i].brushColor.GetColor());

            if (brushHex == detectedHex)
            {
                Debug.Log($"Found BrushColor for {detectedHex}. Returning PlayerStats reference.");
                return colorDetails[i].brushColor.GetPlayerStats();
            }
        }

        Debug.LogWarning($"No matching BrushColor found for {detectedHex}! Returning default stats.");
        return ScriptableObject.CreateInstance<PlayerStats>();
    }
}
