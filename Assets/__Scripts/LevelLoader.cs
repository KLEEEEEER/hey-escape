using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject currentLevel;

    private int currentLevelIndex = 0;

    private static LevelLoader s_Instance = null;

    public static LevelLoader instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
            }

            if (s_Instance == null)
            {
                var obj = new GameObject("LevelLoader");
                s_Instance = obj.AddComponent<LevelLoader>();
            }

            return s_Instance;
        }
    }

    private void Start()
    {
        LoadLevel();
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex + 1 >= levels.Length) 
        {
            //Debug.Log("currentLevelIndex  + 1 (" + currentLevelIndex + 1 + ") > levels.Length (" + levels.Length + ")");
            return; 
        }
        currentLevelIndex++;
        LoadLevel();
    }

    public void LoadFirstLevel()
    {
        currentLevelIndex = 0;
        LoadLevel();
    }

    private void LoadLevel()
    {
        CleanCurrentLevelObject();
        GameObject newLoadedLevel = Instantiate(levels[currentLevelIndex], currentLevel.transform);
        Transform startPosition = newLoadedLevel.transform.Find("StartPosition");
        GameManager.instance.Player.position = startPosition.position;
    }

    private void CleanCurrentLevelObject()
    {
        if (currentLevel.transform.childCount == 0) return;

        for (int i = 0; i < currentLevel.transform.childCount; i++)
        {
            Transform child = currentLevel.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
