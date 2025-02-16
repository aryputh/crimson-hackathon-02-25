using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Vector2 spawnPoint;
    [SerializeField] private GameObject groundChecker;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ColorManager colorManager;

    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = spawnPoint;
    }

    private void Update()
    {
        HandleMovement();

        isGrounded = Physics2D.OverlapCircle(groundChecker.transform.position, groundCheckRadius, groundLayer);
        
        if (isGrounded)
        {
            UpdatePlayerStatsBasedOnGround();
        }

        if (Input.GetButtonDown("Reset"))
        {
            DestroyLines();
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");

        Vector2 movementVector = new Vector2(x, 0);
        rb.velocity = new Vector2(movementVector.normalized.x * playerStats.MovementSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * playerStats.JumpSpeed, ForceMode2D.Impulse);
        }

        animator.SetBool("isJumping", !isGrounded);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void UpdatePlayerStatsBasedOnGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundChecker.transform.position, groundCheckRadius, groundLayer);

        if (hit != null)
        {
            LineRenderer lineRenderer = hit.GetComponent<LineRenderer>();

            if (lineRenderer != null)
            {
                Color groundColor = lineRenderer.startColor;

                playerStats = colorManager.GetStatsFromColor(groundColor);
            }
        }
    }

    private void DestroyLines()
    {
        Line[] lines = FindObjectsByType<Line>(FindObjectsSortMode.None);

        foreach (Line line in lines)
        {
            Destroy(line.gameObject);
        }
    }

    public Vector2 SpawnPoint => spawnPoint;
}
