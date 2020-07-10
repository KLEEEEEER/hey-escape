using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum State { GameOver, Playing, Paused }
    State currentState = State.GameOver;

    public Transform Player;
    public PlayerMovement PlayerMovement;
    public CharacterController2D CharacterController2D;
    public Player PlayerComponent;
    public SpriteRenderer PlayerRenderer;
    public Rigidbody2D PlayerRigidbody;
    public Vector3 PlayerInitialScale;
    public Color PlayerInitialColor;
    public Camera MainCamera;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject youWonScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Text currentTimeText;
    [SerializeField] private Text CountDownTimer;
    [SerializeField] private Text HighscoreText;
    [SerializeField] private int startCountdownTime = 3;

    private bool isGameOver = false;
    public bool IsGameOver {
        get => currentState == State.GameOver;
    }

    public UnityEvent OnGameOverEvent;
    public UnityEvent OnGameWonEvent;
    public UnityEvent OnGameManagerLoaded;

    private float currentTime;

    private void Start()
    {
        Time.timeScale = 1;
        PlayerInitialScale = Player.localScale;
        PlayerRenderer = Player.GetComponent<SpriteRenderer>();
        PlayerRigidbody = Player.GetComponent<Rigidbody2D>();
        PlayerInitialColor = PlayerRenderer.color;

        if (OnGameOverEvent == null)
            OnGameOverEvent = new UnityEvent();
        if (OnGameWonEvent == null)
            OnGameWonEvent = new UnityEvent();
        if (OnGameManagerLoaded == null)
            OnGameManagerLoaded = new UnityEvent();

        MainCamera = Camera.main;
        OnGameManagerLoaded.Invoke();

        StartCoroutine(StartingCountdown());
    }

    IEnumerator StartingCountdown()
    {
        isGameOver = true;
        for (int i = startCountdownTime; i > 0; i--)
        {
            CountDownTimer.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        CountDownTimer.gameObject.SetActive(false);
        isGameOver = false;
        currentState = State.Playing;
    }

    private void Update()
    {
        if (isGameOver) return;

        currentTime += Time.deltaTime;

        /*if (currentTimeText != null)
            currentTimeText.text = GetCurrentTimeString();*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (isGameOver) return;

        if (currentState == State.Paused)
        {
            pauseScreen.SetActive(false);
            currentState = State.Playing;
            Time.timeScale = 1;
        }
        else
        {
            pauseScreen.SetActive(true);
            currentState = State.Paused;
            Time.timeScale = 0;
        }
    }

    private static GameManager s_Instance = null;

    public static GameManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            return s_Instance;
        }
    }

    public void GameOver()
    {
        currentState = State.GameOver;
        isGameOver = true;
        gameOverScreen.SetActive(true);
        currentTimeText.gameObject.SetActive(false);
        OnGameOverEvent.Invoke();
    }

    public void GameWon()
    {
        currentState = State.GameOver;
        isGameOver = true;
        youWonScreen.SetActive(true);
        currentTimeText.gameObject.SetActive(false);
        if (PlayerPrefs.GetFloat("BestTime", float.PositiveInfinity) > currentTime)
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
        }
        HighscoreText.text = PlayerPrefs.GetFloat("BestTime").ToString("f3");
        OnGameWonEvent.Invoke();
    }

    public string GetCurrentTimeString()
    {
        return currentTime.ToString("f3");
    }

    void OnApplicationQuit()
    {
        s_Instance = null;
    }

    public int GetStartCountdownTime()
    {
        return startCountdownTime;
    }
}
