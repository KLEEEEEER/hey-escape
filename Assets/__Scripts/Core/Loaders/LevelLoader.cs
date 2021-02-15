using HeyEscape.Core.Game;
using HeyEscape.Core.Helpers;
using HeyEscape.Core.Player;
using HeyEscape.Core.Player.FSM;
using HeyEscape.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeyEscape.Core.Loaders
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private GameObject[] tutorialLevels;
        [SerializeField] private GameObject[] levels;
        [SerializeField] private GameObject currentLevel;
        [SerializeField] private PlayerFSM player;
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private bool useCountdown = true;
        [SerializeField] private int countdownTime = 3;

        [Header("Components")]
        [SerializeField] private GameStateChanger gameStateChanger;
        [SerializeField] private BeforePlayTimer beforePlayTimer;
        [SerializeField] private Stopwatch stopwatch;


        private int currentLevelIndex = 0;
        private int tutorialLevelIndex = 0;

        private static LevelLoader s_Instance = null;

        private bool isTutorialApproved = true;
        private bool inTutorial = false;
        private bool cameFromTutorial = false;

        private bool countdownRaised = false;

        public static LevelLoader instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
                }

                return s_Instance;
            }
        }

        private void OnDestroy()
        {
            s_Instance = null;
        }

        public void StartLoading()
        {
            currentLevelIndex = startingLevel - 1;
            if (levels[currentLevelIndex] == null) currentLevelIndex = 0;

            isTutorialApproved = (PlayerPrefs.GetInt("tutorial", 1) == 1);

            LoadLevel();
        }

        public void LoadNextLevel()
        {
            if (currentLevelIndex + 1 >= levels.Length)
            {
                GameStateChanger.Instance.SetState(GameStateChanger.GameState.GameWon);
                return;
            }
            LoadLevel();
        }

        public void LoadFirstLevel()
        {
            currentLevelIndex = 0;
            tutorialLevelIndex = 0;
            SceneManager.LoadScene("LevelLoader");
        }

        public void ToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }

        private void LoadLevel()
        {
            CleanCurrentLevelObject();

            if (isTutorialApproved && tutorialLevels.Length > 0 && tutorialLevelIndex < tutorialLevels.Length)
            {
                inTutorial = true;
                cameFromTutorial = true;
                InstantiateCurrentLevel(tutorialLevels, tutorialLevelIndex);
                tutorialLevelIndex++;
            }
            else
            {
                if (cameFromTutorial)
                {
                    PlayerPrefs.SetInt("tutorial", 0);
                    cameFromTutorial = false;
                }

                if (!countdownRaised && useCountdown)
                {
                    countdownRaised = true;
                    beforePlayTimer.StartCountdown(countdownTime, () =>
                    {
                        gameStateChanger.SetState(GameStateChanger.GameState.Playing);
                        stopwatch.StartCounting();
                    });
                }
                else
                {
                    gameStateChanger.SetState(GameStateChanger.GameState.Playing);
                }

                inTutorial = false;
                InstantiateCurrentLevel(levels, currentLevelIndex);
                currentLevelIndex++;
            }

            LevelFader.instance.FadeIn();
        }

        private void InstantiateCurrentLevel(GameObject[] levelsSource, int levelIndex)
        {
            Level levelComponent = levelsSource[levelIndex].GetComponent<Level>();
            if (levelComponent != null)
            {
                Instantiate(levelsSource[levelIndex], currentLevel.transform);
                if (player != null)
                {
                    player.gameObject.SetActive(false);
                    player.transform.position = levelComponent.startPlayerPosition.position;
                    player.gameObject.SetActive(true);
                }
            }
        }

        private void CleanCurrentLevelObject()
        {
            if (currentLevel.transform.childCount == 0) return;

            for (int i = 0; i < currentLevel.transform.childCount; i++)
            {
                Transform child = currentLevel.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        public int GetCurrentLevelNumber()
        {
            return currentLevelIndex + 1;
        }

        public int GetLevelsCount()
        {
            return levels.Length;
        }


        #region Cheats
#if UNITY_EDITOR
        [MenuItem("Cheats/Levels/Load next level")]
        private static void ToNextLevel()
        {
            if (!Application.isPlaying)
            {
                UnityEngine.Debug.Log("You are not playing right now!");
                return;
            }
            instance.LoadNextLevel();
        }
#endif
        #endregion
    }
}