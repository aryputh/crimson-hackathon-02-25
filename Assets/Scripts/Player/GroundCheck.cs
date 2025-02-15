using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGrounded;

    private void Awake()
    {
        isGrounded = true;
    }

    public bool IsGrounded
    {
        get => isGrounded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGrounded)
        {
            Debug.Log("Grounded.");
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isGrounded)
        {
            Debug.Log("In the air.");
            isGrounded = false;
        }
    }
}
