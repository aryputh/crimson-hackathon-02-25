using System.Collections.Generic;
using UnityEngine;

public enum World
{
    none,
    forest
}

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance;

    private const string WorldProgressKey = "WorldProgress_";
    private Dictionary<World, int> worldProgress = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsLevelUnlocked(World world, int level)
    {
        return worldProgress.TryGetValue(world, out int unlockedLevel) && level <= unlockedLevel;
    }

    public bool IsWorldUnlocked(World world)
    {
        return worldProgress.TryGetValue(world, out int unlockedLevel) && unlockedLevel > 0;
    }

    public void CompleteLevel(World world, int level, int totalLevelsInWorld, World? nextWorld = null)
    {
        if (!worldProgress.ContainsKey(world))
            worldProgress[world] = 1;

        if (worldProgress[world] <= level)
        {
            worldProgress[world] = level + 1;
            SaveProgress(world);

            if (level + 1 > totalLevelsInWorld && nextWorld.HasValue)
            {
                UnlockFirstLevel(nextWorld.Value);
            }
        }
    }

    private void UnlockFirstLevel(World world)
    {
        if (!worldProgress.ContainsKey(world))
        {
            worldProgress[world] = 1;
            SaveProgress(world);
        }
    }

    private void SaveProgress(World world)
    {
        PlayerPrefs.SetInt(WorldProgressKey + world.ToString(), worldProgress[world]);
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        foreach (World world in System.Enum.GetValues(typeof(World)))
        {
            int progress = PlayerPrefs.GetInt(WorldProgressKey + world.ToString(), 1);
            worldProgress[world] = progress;
        }
    }
}
