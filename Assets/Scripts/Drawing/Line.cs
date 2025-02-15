using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider;

    private readonly List<Vector2> points = new List<Vector2>();

    private void Start()
    {
        edgeCollider.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos))
        {
            return;
        }

        points.Add(pos);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, pos);

        edgeCollider.points = points.ToArray();
    }

    private bool CanAppend(Vector2 pos)
    {
        if (points.Count == 0)
        {
            return true;
        }

        return Vector2.Distance(points[points.Count - 1], pos) > DrawManager.RESOLUTION;
    }
}
