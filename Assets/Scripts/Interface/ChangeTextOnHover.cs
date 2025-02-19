using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChangeTextOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color hoverColor;
    [SerializeField] private float colorChangeSpeed = 0.1f;

    private Color originalColor;
    private Coroutine colorChangeCoroutine;

    private void Start()
    {
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        if (text != null)
        {
            originalColor = text.color; // Store the original color
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text != null)
        {
            if (colorChangeCoroutine != null) StopCoroutine(colorChangeCoroutine);
            colorChangeCoroutine = StartCoroutine(ChangeTextColor(hoverColor, colorChangeSpeed));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (text != null)
        {
            if (colorChangeCoroutine != null) StopCoroutine(colorChangeCoroutine);
            colorChangeCoroutine = StartCoroutine(ChangeTextColor(originalColor, colorChangeSpeed));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (text != null)
        {
            text.color = originalColor; // Reset on click
        }
    }

    private IEnumerator ChangeTextColor(Color targetColor, float duration)
    {
        float time = 0;
        Color startColor = text.color;

        while (time < duration)
        {
            text.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        text.color = targetColor; // Ensure final color is set
    }
}
