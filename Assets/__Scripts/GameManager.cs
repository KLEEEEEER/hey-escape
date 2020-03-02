using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform Player;
    public PlayerMovement PlayerMovement;
    public Player PlayerComponent;
    public SpriteRenderer PlayerRenderer;
    public Rigidbody2D PlayerRigidbody;
    public Vector3 PlayerInitialScale;
    public Color PlayerInitialColor;
    public Camera MainCamera;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject youWonScreen;
    [SerializeField] private Text currentTimeText;
    [SerializeField] private Text CountDownTimer;
    [SerializeField] private Text HighscoreText;
    [SerializeField] private int startCountdownTime = 3;

    private bool isGameOver = false;
    public bool IsGameOver {
        get => isGameOver;
    }

    public UnityEvent OnGameOverEvent;
    public UnityEvent OnGameWonEvent;

    private float currentTime;

    private void Start()
    {
        PlayerInitialScale = Player.localScale;
        PlayerRenderer = Player.GetComponent<SpriteRenderer>();
        PlayerRigidbody = Player.GetComponent<Rigidbody2D>();
        PlayerInitialColor = PlayerRenderer.color;

        if (OnGameOverEvent == null)
            OnGameOverEvent = new UnityEvent();
        if (OnGameWonEvent == null)
            OnGameWonEvent = new UnityEvent();

        MainCamera = Camera.main;

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
    }

    private void Update()
    {
        if (isGameOver) return;

        currentTime += Time.deltaTime;
        currentTimeText.text = GetCurrentTimeString();
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

            if (s_Instance == null)
            {
                var obj = new GameObject("GameManager");
                s_Instance = obj.AddComponent<GameManager>();
            }

            return s_Instance;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        currentTimeText.gameObject.SetActive(false);
        OnGameOverEvent.Invoke();
    }

    public void GameWon()
    {
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
