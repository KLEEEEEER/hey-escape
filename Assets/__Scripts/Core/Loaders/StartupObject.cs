﻿namespace HeyEscape.Core.Loaders
{
    public class StartupObject
    {
        public static bool IsSettingsSet = false;

        public static void SetSettings()
        {
            if (!IsSettingsSet)
            {
                //Application.targetFrameRate = 5000;
                //QualitySettings.vSyncCount = 0;
                //Screen.SetResolution(Mathf.RoundToInt(Screen.width * 0.6f), Mathf.RoundToInt(Screen.height * 0.6f), true);
                IsSettingsSet = true;
            }
        }
    }
}