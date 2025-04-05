using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WorldButton : MonoBehaviour
{
    public World world;
    
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        bool unlocked = LevelProgressManager.Instance.IsWorldUnlocked(world);

        button.interactable = unlocked;
    }
}
