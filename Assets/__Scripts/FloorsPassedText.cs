using UnityEngine;
using UnityEngine.UI;

public class FloorsPassedText : MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void OnGameOver()
    {
        int levelsCount = LevelLoader.instance.GetLevelsCount();
        int currentLevel = LevelLoader.instance.GetCurrentLevelNumber();
        text.text = $"You stopped on {currentLevel} out of {levelsCount} floors.";
    }
}
