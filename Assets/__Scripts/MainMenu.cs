using KleeeeeerUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject[] allScreens;

    [SerializeField] private SettingsBehaviour settingsBehaviour;
    private void Start()
    {
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

    public void OpenSettingsPage()
    {
        closeAllScreens();
        settingsScreen.SetActive(true);
    }

    public void OpenMainMenu()
    {
        closeAllScreens();
        mainMenuScreen.SetActive(true);
    }

    private void closeAllScreens()
    {
        foreach (GameObject screen in allScreens)
        {
            screen.SetActive(false);
        }
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
