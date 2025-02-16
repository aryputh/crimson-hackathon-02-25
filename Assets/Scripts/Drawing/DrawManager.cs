using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab;
    [SerializeField] private ColorManager colorManager;

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
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && (hit.collider.gameObject.layer == LayerMask.NameToLayer("Default") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")))
            {
                currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                currentLine.SetPosition(mousePos);
                currentLine.GetComponent<LineRenderer>().startColor = colorManager.GetCurrentColor().GetColorHex();
                currentLine.GetComponent<LineRenderer>().endColor = colorManager.GetCurrentColor().GetColorHex();
            }
        }

        if (Input.GetMouseButton(0) && currentLine != null)
        {
            if (hit.collider != null && (hit.collider.gameObject.layer == LayerMask.NameToLayer("Default") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")))
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
