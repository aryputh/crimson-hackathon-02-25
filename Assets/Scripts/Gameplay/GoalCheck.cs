using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCheck : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private LevelUnlocker levelUnlocker;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Successful level completion, go to next screen.
            levelUnlocker.OnGoalReached();
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
