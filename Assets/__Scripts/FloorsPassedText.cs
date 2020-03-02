using UnityEngine;
using UnityEngine.UI;

public class FloorsPassedText : MonoBehaviour
{
    private void OnEnable()
    {
        Text text = GetComponent<Text>();
        int levelsCount = LevelLoader.instance.GetLevelsCount();
        int currentLevel = LevelLoader.instance.GetCurrentLevelNumber();
        text.text = $"You stopped on {currentLevel} out of {levelsCount} floors.";
    }
}
