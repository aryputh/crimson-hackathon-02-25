using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private World world = World.none;
    [SerializeField] private int level = -1;
    [SerializeField] private TMP_Text levelNumText;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        bool unlocked = LevelProgressManager.Instance.IsLevelUnlocked(world, level);
        levelNumText.text = level.ToString();

        button.interactable = unlocked;
    }
}
