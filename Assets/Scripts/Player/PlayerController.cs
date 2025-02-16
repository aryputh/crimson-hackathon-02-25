using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Vector2 spawnPoint;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        transform.position = spawnPoint;
    }

    private void Update()
    {
        HandleMovement();

        if (Input.GetButtonDown("Reset"))
        {
            DestroyLines();
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");

        Vector2 movementVector = new Vector2(x, 0);

        rb.velocityX = movementVector.normalized.x * playerStats.Speed;

        if (Input.GetButtonDown("Jump") && groundCheck.IsGrounded)
        {
            rb.AddForce(Vector2.up * playerStats.JumpSpeed);
        }
    }

    public void ChangePlayerStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    private void DestroyLines()
    {
        Debug.Log("Destroying lines.");
        Line[] lines = FindObjectsByType<Line>(FindObjectsSortMode.None);
        
        foreach (Line l in lines)
        {
            Destroy(l.gameObject);
        }
    }

    public Vector2 SpawnPoint
    {
        get => spawnPoint;
    }
}
