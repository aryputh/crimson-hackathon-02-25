using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCheck : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Successful level completion, go to next screen.
            Debug.Log("Go to next level!");
        }
        GoToNextLevel();
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }
}
