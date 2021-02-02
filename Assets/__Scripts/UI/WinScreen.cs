using HeyEscape.Core.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeyEscape.UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private Stopwatch stopwatch;
        [SerializeField] private Text timeSpentText;
        [SerializeField] private Text highscoreText;

        /*private void OnEnable()
        {
            timeSpentText.text = stopwatch.GetCurrentValue().ToString("f3");
        }*/

        public void OnVictory()
        {
            float currentTime = stopwatch.GetCurrentValue();
            timeSpentText.text = currentTime.ToString("f3"); 
            
            if (PlayerPrefs.GetFloat("BestTime", float.PositiveInfinity) > currentTime)
            {
                PlayerPrefs.SetFloat("BestTime", currentTime);
            }
            highscoreText.text = PlayerPrefs.GetFloat("BestTime").ToString("f3");
        }
    }
}