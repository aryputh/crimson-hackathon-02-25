using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab;
    [SerializeField] private ColorManager colorManager;

    [Header("Layer Settings")]
    [SerializeField] private LayerMask drawableLayers;
    [SerializeField] private LayerMask noDrawLayers;

    private Camera cam;
    private Line currentLine;

    public const float RESOLUTION = 0.1f;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0.1f);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & noDrawLayers) == 0)
            {
                currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                currentLine.SetPosition(mousePos);

                BrushColor brushColor = colorManager.GetCurrentColor();
                if (brushColor != null)
                {
                    LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
                    lineRenderer.startColor = brushColor.Color;
                    lineRenderer.endColor = brushColor.Color;
                }
            }
        }

        if (Input.GetMouseButton(0) && currentLine != null)
        {
            if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & noDrawLayers) == 0)
            {
                currentLine.SetPosition(mousePos);
            }
            else
            {
                currentLine = null;
            }
        }
    }
}
