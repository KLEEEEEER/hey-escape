using KleeeeeerUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenu : MenuPageChanger
{
    [SerializeField] private SettingsBehaviour settingsBehaviour;
    [SerializeField] private GameObject startWithTutorialButton;
    private void Start()
    {
        if (PlayerPrefs.GetInt("tutorial", 0) == 0)
        {
            startWithTutorialButton.SetActive(true);
        }
        else
        {
            startWithTutorialButton.SetActive(false);
        }

        StartupObject.SetSettings();

        LevelFader.instance.FadeIn();
        settingsBehaviour.setStartMixerPosition();
    }

    public void LoadLevelLoader()
    {
        StartCoroutine(LoadLevelLoaderCoroutine());
    }

    public void LoadLevelLoaderWithTutorial()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        StartCoroutine(LoadLevelLoaderCoroutine());
    }

    IEnumerator LoadLevelLoaderCoroutine()
    {
        LevelFader.instance.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LevelLoader");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
