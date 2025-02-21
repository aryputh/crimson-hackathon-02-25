using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Runs all behavior for the player character.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // --- SERIALIZED VARIABLES --- //
    // Contains data on the player's walk speed, jump speed, and gravity influence.
    [SerializeField] private PlayerStats playerStats;
    // Contains data regarding the spawn point of the player on reset or level start.
    [SerializeField] private Vector2 spawnPoint;
    // An object that is created to check if the player is standing near the ground.
    [SerializeField] private GameObject groundChecker;
    // A float representing the radius of the circle for the groundChecker object to scan.
    [SerializeField] private float groundCheckRadius;
    // Information regarding all objects on the ground layer.
    [SerializeField] private LayerMask groundLayer;
    // A reference to the ColorManager script.
    [SerializeField] private ColorManager colorManager;

    // --- PRIVATE VARIABLES --- //
    // Determines if the player is on the ground based on the CircleCast of groundChecker.
    private bool isGrounded;
    // Determines which platform gravity influence applies to the player.
    private int currentGravityInfluence;
    // Floats that track how long the player can walk off the platform and still jump.
    private float coyoteTime = 0.25f;
    private float coyoteTimeTracker;
    // Floats that track how soon before the player hits the ground that the player can jump.
    private float jumpBuffer = 0.25f;
    private float jumpBufferTracker;
    // Tracks the default color stats (starts as black color stats)
    private PlayerStats defaultStats;
    // The Rigidbody of the Player object.
    private Rigidbody2D rb;
    // The component controlling the animation of the Player.
    private Animator animator;
    // Manages the Sprite component orientation.
    private SpriteRenderer sr;

    /// <summary>
    /// Gets the Player's current spawn point.
    /// </summary>
    public Vector2 SpawnPoint => spawnPoint;

    /// <summary>
    /// Unity's native function for operations to be executed upon the object's enabling/
    /// instantiation.
    /// </summary>
    private void Awake()
    {
        // Sets the defaultStats to whatever is set in the Inspector window.
        defaultStats = playerStats;
        // Obtains information regarding the player's gravity influence from base stats.
        currentGravityInfluence = playerStats.GravityInfluence;
        // Grabs the various components needed from the Player object.
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        // Places the Player at the designated spawnPoint on the canvas.
        transform.position = spawnPoint;
    }

    /// <summary>
    /// Unity's native function that is called roughly once per frame so long as the object
    /// exists.
    /// </summary>
    private void Update()
    {
        // Determine the overall movement of the Player on the current timestep.
        HandleMovement();
        // Checks if the groundChecker object currently overlaps the current canvas ground or
        // a drawn platform.
        isGrounded = Physics2D.OverlapCircle(groundChecker.transform.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            // If grounded, reset the coyote time as the player is not falling.
            coyoteTimeTracker = coyoteTime;
            // Check what ground type the player is standing on and updates the Player's stats.
            UpdatePlayerStatsBasedOnGround();
        }
        else
        {
            // Lowers the coyote time countdown as long as the player is currently falling.
            coyoteTimeTracker -= Time.deltaTime;
        }

        // Reset is bound to 'R'.
        if (Input.GetButtonDown("Reset"))
        {
            // Destroy all line objects currently in the level.
            DestroyLines();
            // Resets player position.
            this.gameObject.transform.position = spawnPoint;
        }
    }

    /// <summary>
    /// Determines the overall speed of the player's movement on the current timestep.
    /// </summary>
    private void HandleMovement()
    {
        // Horizontal axis is bound to A/D and LeftArrow/RightArrow
        float x = Input.GetAxis("Horizontal");

        // Creates a new movement Vector to determine the direction the player should go.
        Vector2 movementVector = new Vector2(x, 0);
        // Moves the player at their movement speed.
        rb.velocity = new Vector2(movementVector.normalized.x * playerStats.MovementSpeed, rb.velocity.y);
        // Flips the sprite if the left key is being pressed.
        sr.flipX = movementVector.x < 0;

        // Sets the animation to idle if the player is on the ground.
        animator.SetBool("isGrounded", isGrounded);

        // Jump is bound to UpArrow/W/Space. The player is also able to jump as long as
        // coyote time is active (grounded or just left the platform)
        if (Input.GetButtonDown("Jump") && coyoteTimeTracker > 0f)
        {
            // Uses the groundChecker object to determine what the player is currently standing on.
            Collider2D collider = Physics2D.OverlapCircle(groundChecker.transform.position, groundCheckRadius, groundLayer);

            // Prevents a bug where the player can wall jump off of the canvas walls.
            if (collider.gameObject.tag != "CanvasEdge")
            {
                // Determines the direction the player jumps based on if the player
                // is upside down or not.
                if (rb.gravityScale > 0)
                {
                    rb.AddForce(Vector2.up * playerStats.JumpSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.down * playerStats.JumpSpeed, ForceMode2D.Impulse);
                }
                // Disables coyote time once the player is airborne.
                coyoteTimeTracker = 0f;
                // Plays the animation for the player jumping.
                animator.SetTrigger("jumped");
                // Implement jump buffer here.
                jumpBufferTracker = jumpBuffer;
            }
        }
        else
        {
            // Determine if the player is still moving laterally, if so play the walking animation.
            if (Mathf.Abs(rb.velocity.x) > 0)
            {
                animator.SetBool("isMovingX", true);
            }
            else
            {
                animator.SetBool("isMovingX", false);
            }
            // Implement jump buffer here.
            jumpBufferTracker -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Changes the gravity scale of the player when on a platform. This function should also
    /// change the gravity scale of the player upon reset.
    /// </summary>
    private void HandleGravityState()
    {
        Debug.Log("Triggered gravity change.");
        // Updates the gravity of the Rigidbody for logic and the Player object itself visually.
        rb.gravityScale = playerStats.GravityInfluence == 1 ? Mathf.Abs(rb.gravityScale) : -1 * Mathf.Abs(rb.gravityScale);
        transform.localScale = playerStats.GravityInfluence == 1 ? new Vector2(1, 1) : new Vector2(1, -1);
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

                    if (playerStats.GravityInfluence != 0 && currentGravityInfluence != playerStats.GravityInfluence)
                    {
                        HandleGravityState();
                    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            DestroyLines();
            this.gameObject.transform.position = spawnPoint;
            rb.gravityScale = Mathf.Abs(rb.gravityScale);
            transform.localScale = new Vector2(1, 1);
        }
    }
}
