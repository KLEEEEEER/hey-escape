using KleeeeeerUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenu : MenuPageChanger
{
    [SerializeField] private SettingsBehaviour settingsBehaviour;
    private void Start()
    {
        StartupObject.SetSettings();

        LevelFader.instance.FadeIn();
        settingsBehaviour.setStartMixerPosition();
    }

    public void LoadLevelLoader()
    {
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
