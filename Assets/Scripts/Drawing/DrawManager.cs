using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab;

    private Camera cam;
    private Line currentLine;

    public const float RESOLUTION = 0.01f;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            currentLine.SetPosition(mousePos);
        }

        if (Input.GetMouseButton(0) && currentLine != null)
        {
            currentLine.SetPosition(mousePos);
        }
    }
}
