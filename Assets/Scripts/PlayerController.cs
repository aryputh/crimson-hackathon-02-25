using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();

    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");

        Vector2 movementVector = new Vector2(x, 0);

        rb.velocityX = movementVector.normalized.x * playerStats.Speed;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector2.up * playerStats.JumpSpeed);
        }
    }
}
