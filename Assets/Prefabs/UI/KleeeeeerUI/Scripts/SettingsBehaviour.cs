using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

namespace KleeeeeerUI
{
    public class SettingsBehaviour : MonoBehaviour
    {
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private EventTrigger masterVolumeSliderDrag;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private EventTrigger musicVolumeSliderDrag;
        [SerializeField] private Slider fxVolumeSlider;
        [SerializeField] private EventTrigger fxVolumeSliderDrag;
        [SerializeField] private Toggle mutedToggle;

        [SerializeField] private AudioMixer masterMixer;

        [SerializeField] private bool isMuted = false;

        [SerializeField] private Vector2 minMaxMasterVolume = new Vector2(-60f, 0f);
        [SerializeField] private Vector2 minMaxMusicVolume = new Vector2(-60f, -17f);
        [SerializeField] private Vector2 minMaxFxVolume = new Vector2(-60f, 0f);

        [SerializeField] Dropdown languageDropdown;

        IEnumerator Start()
        {
            Debug.Log("SettingsBehaviour Start");
            yield return LocalizationSettings.InitializationOperation;

            var options = new List<Dropdown.OptionData>();
            int selected = 0;
            for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
            {
                var locale = LocalizationSettings.AvailableLocales.Locales[i];
                if (LocalizationSettings.SelectedLocale == locale)
                    selected = i;
                options.Add(new Dropdown.OptionData(locale.name));
            }
            languageDropdown.options = options;

            languageDropdown.value = selected;
            languageDropdown.onValueChanged.AddListener(LocaleSelected);
        }

        static void LocaleSelected(int index)
        {
            PlayerPrefs.SetInt("language_index", index);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }

        private void OnEnable()
        {
            masterVolumeSlider.onValueChanged.AddListener(masterVolumeChanged);
            
            musicVolumeSlider.onValueChanged.AddListener(musicVolumeChanged);
            fxVolumeSlider.onValueChanged.AddListener(fxVolumeChanged);
            mutedToggle.onValueChanged.AddListener(muteSoundToggle);

            masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume", minMaxMasterVolume.y);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume", minMaxMusicVolume.y);
            fxVolumeSlider.value = PlayerPrefs.GetFloat("fxVolume", minMaxFxVolume.y);
            mutedToggle.isOn = (PlayerPrefs.GetFloat("mutedVolume", 0f) == -80f);
        }

        private void OnDisable()
        {
            masterVolumeSlider.onValueChanged.RemoveListener(masterVolumeChanged);
            musicVolumeSlider.onValueChanged.RemoveListener(musicVolumeChanged);
            fxVolumeSlider.onValueChanged.RemoveListener(fxVolumeChanged);
            mutedToggle.onValueChanged.RemoveListener(muteSoundToggle);
        }

        private void masterVolumeChanged(float value)
        {
            masterMixer.SetFloat("masterVolume", value);
            PlayerPrefs.SetFloat("masterVolume", value);
        }

        private void musicVolumeChanged(float value)
        {
            masterMixer.SetFloat("musicVolume", value);
            PlayerPrefs.SetFloat("musicVolume", value);
        }

        private void fxVolumeChanged(float value)
        {
            masterMixer.SetFloat("fxVolume", value);
            PlayerPrefs.SetFloat("fxVolume", value);
        }

        private void muteSoundToggle(bool isMutedValue)
        {
            isMuted = isMutedValue;
            masterMixer.SetFloat("mutedVolume", ((isMuted) ? -80f : 0f));
            PlayerPrefs.SetFloat("mutedVolume", ((isMuted) ? -80f : 0f));
        }

        public void setStartMixerPosition()
        {
            masterMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume", minMaxMasterVolume.y));
            masterMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume", minMaxMusicVolume.y));
            masterMixer.SetFloat("fxVolume", PlayerPrefs.GetFloat("fxVolume", minMaxFxVolume.y));
            masterMixer.SetFloat("mutedVolume", PlayerPrefs.GetFloat("mutedVolume", 0f));
        }
    }
}