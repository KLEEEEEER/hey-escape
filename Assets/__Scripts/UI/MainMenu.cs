﻿using HeyEscape.Core.Loaders;
using KleeeeeerUI;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

namespace HeyEscape.UI
{
    public class MainMenu : MenuPageChanger
    {
        [SerializeField] private SettingsBehaviour settingsBehaviour;
        [SerializeField] private GameObject startWithTutorialButton;

        private WaitForSeconds delayBeforeLocalization = new WaitForSeconds(0.5f);
        private WaitForSeconds delayFadeOut = new WaitForSeconds(1f);
        IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("language_index", 0)];

            yield return delayBeforeLocalization;

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
            yield return delayFadeOut;
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
}