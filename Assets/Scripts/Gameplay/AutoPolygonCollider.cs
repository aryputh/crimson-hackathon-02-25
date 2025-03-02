using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D), typeof(SpriteRenderer))]
public class SpritePolygonCollider : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Make sure everything is set correctly
        if (polygonCollider != null && spriteRenderer.sprite != null)
        {
            UpdateColliderToSprite();
        }
    }

    void UpdateColliderToSprite()
    {
        // Get physics shape data from Unity's built-in system
        int shapeCount = spriteRenderer.sprite.GetPhysicsShapeCount();
        List<Vector2> path = new List<Vector2>();

        polygonCollider.pathCount = shapeCount;

        // Create the path for the colliders
        for (int i = 0; i < shapeCount; i++)
        {
            path.Clear();
            spriteRenderer.sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        }
    }
}