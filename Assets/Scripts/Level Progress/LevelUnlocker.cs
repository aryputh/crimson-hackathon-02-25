using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private World currentWorld = World.none;
    [SerializeField] private int currentLevel;
    [SerializeField] private int totalLevelsInWorld;
    [SerializeField] private World nextWorld;
    public bool hasNextWorld;

    private bool isAlreadyCompleted;

    private void Start()
    {
        isAlreadyCompleted = LevelProgressManager.Instance.IsLevelUnlocked(currentWorld, currentLevel + 1);
    }

    public void OnGoalReached()
    {
        if (!isAlreadyCompleted)
        {
            LevelProgressManager.Instance.CompleteLevel(
                currentWorld,
                currentLevel,
                totalLevelsInWorld,
                hasNextWorld ? nextWorld : (World?)null
            );
        }
    }
}
