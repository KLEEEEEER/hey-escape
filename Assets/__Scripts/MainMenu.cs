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
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("language_index", 0)];

        yield return new WaitForSeconds(0.5f);

        if (PlayerPrefs.GetInt("tutorial", 1) == 0)
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

    public void OpenGithub()
    {
        Application.OpenURL("https://github.com/KLEEEEEER");
    }

    public void OpenItchio()
    {
        Application.OpenURL("https://maxnitals.itch.io/");
    }

    public void OpenSoundcloud()
    {
        Application.OpenURL("https://soundcloud.com/maxnitals");
    }
}
