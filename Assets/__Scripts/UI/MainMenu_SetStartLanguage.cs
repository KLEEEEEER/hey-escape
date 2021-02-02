using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace HeyEscape.UI
{
    public class MainMenu_SetStartLanguage : MonoBehaviour
    {
        IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("language_index", 0)];
        }

    }
}