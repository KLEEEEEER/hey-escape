using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private bool isGameOver = false;
    public bool IsGameOver {
        get => isGameOver;
    }
    public UnityEvent OnGameOverEvent;

    private void Start()
    {
        PlayerInitialScale = Player.localScale;
        PlayerRenderer = Player.GetComponent<SpriteRenderer>();
        PlayerRigidbody = Player.GetComponent<Rigidbody2D>();
        PlayerInitialColor = PlayerRenderer.color;

        if (OnGameOverEvent == null)
            OnGameOverEvent = new UnityEvent();

        MainCamera = Camera.main;
    }

    private void Update()
    {
        if (!isGameOver) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private static GameManager s_Instance = null;

    public static GameManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                // FindObjectOfType() returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            // If it is still null, create a new instance
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
        OnGameOverEvent.Invoke();
        gameOverScreen.SetActive(true);
    }

    void OnApplicationQuit()
    {
        s_Instance = null;
    }
}
