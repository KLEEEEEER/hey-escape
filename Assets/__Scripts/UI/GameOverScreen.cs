using HeyEscape.Core.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace HeyEscape.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Stopwatch stopwatch;
        [SerializeField] private Text timeSpentText;

        public void OnGameOver()
        {
            float currentTime = stopwatch.GetCurrentValue();
            timeSpentText.text = currentTime.ToString("f3");
        }
    }
}