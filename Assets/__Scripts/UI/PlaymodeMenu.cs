using HeyEscape.Core.Game;
using HeyEscape.Core.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace HeyEscape.UI
{
    public class PlaymodeMenu : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject youWonScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private Text HighscoreText;

        [SerializeField] private Stopwatch stopwatch;

        private bool isPaused = false;
        public void TogglePauseMenu()
        {
            if (isPaused)
            {
                GameStateChanger.Instance.SetState(GameStateChanger.GameState.Playing);
                isPaused = !isPaused;
            }
            else
            {
                GameStateChanger.Instance.SetState(GameStateChanger.GameState.Paused);
                isPaused = !isPaused;
            }
        }

        public void OnPause()
        {
            stopwatch.Pause();
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }

        public void OnPlaying()
        {
            stopwatch.Continue();
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }

        public void OnGameOver()
        {
            gameOverScreen.SetActive(true);
        }

        public void OnWin()
        {
            float currentTime = stopwatch.GetCurrentValue();

            if (PlayerPrefs.GetFloat("BestTime", float.PositiveInfinity) > currentTime)
            {
                PlayerPrefs.SetFloat("BestTime", currentTime);
            }
            HighscoreText.text = PlayerPrefs.GetFloat("BestTime").ToString("f3");
            youWonScreen.SetActive(true);
        }
    }
}