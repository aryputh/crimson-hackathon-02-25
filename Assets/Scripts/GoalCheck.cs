using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Successful level completion, go to next screen.
            Debug.Log("Go to next level!");
        }
        else
        {
            Debug.Log("Triggered but not found.");
        }
    }
}
