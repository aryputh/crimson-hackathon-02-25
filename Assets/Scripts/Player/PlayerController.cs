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
    private bool previousGravityState;
    private PlayerStats defaultStats;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private void Awake()
    {
        defaultStats = playerStats;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        transform.position = spawnPoint;

        previousGravityState = playerStats.TogglesGravity;
        HandleGravityState();
    }

    private void Update()
    {
        HandleMovement();

        isGrounded = Physics2D.OverlapCircle(groundChecker.transform.position, groundCheckRadius, groundLayer);

        if (previousGravityState != playerStats.TogglesGravity)
        {
            Debug.Log("Updating from update function.");
            previousGravityState = playerStats.TogglesGravity;
            HandleGravityState();
        }

        if (isGrounded)
        {
            UpdatePlayerStatsBasedOnGround();
        }

        if (Input.GetButtonDown("Reset"))
        {
            DestroyLines();
            this.gameObject.transform.position = spawnPoint;
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");

        Vector2 movementVector = new Vector2(x, 0);
        rb.velocity = new Vector2(movementVector.normalized.x * playerStats.MovementSpeed, rb.velocity.y);
        sr.flipX = movementVector.x < 0;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (playerStats.TogglesGravity)
            {
                rb.AddForce(Vector2.down * playerStats.JumpSpeed, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * playerStats.JumpSpeed, ForceMode2D.Impulse);
            }

            animator.Play("Jumping");
        }
        else if (rb.velocity.y < -1)
        {
            animator.Play("Falling");
        }
        else if (Mathf.Abs(rb.velocity.x) > 0)
        {
            animator.Play("Walking");
        }
        else
        {
            animator.Play("Idling");
        }
    }

    private void HandleGravityState()
    {
        Debug.Log("Triggered gravity change.");
        rb.gravityScale *= playerStats.TogglesGravity ? -1 : 1;
        transform.localScale = playerStats.TogglesGravity ? new Vector2(1, -1) : new Vector2 (1, 1);
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

                Debug.Log($"Ground detected: {groundColor}");

                PlayerStats statsFromColor = colorManager.GetStatsFromColor(groundColor);

                if (statsFromColor != null)
                {
                    playerStats = statsFromColor;
                    Debug.Log($"Player stats updated: Speed = {playerStats.MovementSpeed}, Jump = {playerStats.JumpSpeed}");
                }
                else
                {
                    Debug.LogWarning("GetStatsFromColor() returned null! Using default stats.");
                    playerStats = defaultStats;
                }
            }
            else
            {
                playerStats = defaultStats;
            }

            if (previousGravityState == true && playerStats.TogglesGravity != true)
            {
                playerStats.TogglesGravity = true;
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
