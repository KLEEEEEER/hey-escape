﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            GameManager.instance.GameWon();
            return; 
        }
        currentLevelIndex++;
        LoadLevel();
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("LevelLoader");
        currentLevelIndex = 0;
        LoadLevel();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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

    public int GetCurrentLevelNumber()
    {
        return currentLevelIndex + 1;
    }

    public int GetLevelsCount()
    {
        return levels.Length;
    }
}
