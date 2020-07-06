using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KleeeeeerUI
{
    public class MenuPageChanger : MonoBehaviour
    {
        [SerializeField] private GameObject[] allScreens;
        [SerializeField] private GameObject settingsScreen;
        [SerializeField] private GameObject mainScreen;

        protected void closeAllScreens()
        {
            foreach (GameObject screen in allScreens)
            {
                screen.SetActive(false);
            }
        }

        public void OpenSettingsPage()
        {
            closeAllScreens();
            settingsScreen.SetActive(true);
        }

        public void OpenMainScreen()
        {
            closeAllScreens();
            mainScreen.SetActive(true);
        }
    }
}