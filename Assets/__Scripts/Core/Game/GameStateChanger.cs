using HeyEscape.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeyEscape.Core.Game
{
    public class GameStateChanger : MonoBehaviour
    {
        public enum GameState { GameOver, Playing, Paused, GameWon, BeforePlay }
        GameState currentState = GameState.BeforePlay;

        public UnityEvent OnGameOverEvent;
        public UnityEvent OnGameWonEvent;
        public UnityEvent OnGamePlayingEvent;
        public UnityEvent OnGamePausedEvent;
        public UnityEvent OnGameBeforePlayEvent;
        public UnityEvent OnGameStateChangedEvent;

        [SerializeField] private InputHandler inputHandler;

        public static GameStateChanger Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                UnityEngine.Debug.LogError("GameStateChanger already in a scene.");
            }

            if (OnGameOverEvent == null)
                OnGameOverEvent = new UnityEvent();
            if (OnGameWonEvent == null)
                OnGameWonEvent = new UnityEvent();
            if (OnGamePlayingEvent == null)
                OnGamePlayingEvent = new UnityEvent();
            if (OnGamePausedEvent == null)
                OnGamePausedEvent = new UnityEvent();
            if (OnGameBeforePlayEvent == null)
                OnGameBeforePlayEvent = new UnityEvent();
            if (OnGameStateChangedEvent == null)
                OnGameStateChangedEvent = new UnityEvent();

            SetState(GameState.BeforePlay);
        }

        public void SetState(GameState state)
        {
            currentState = state;
            OnGameStateChangedEvent.Invoke();

            switch (currentState)
            {
                case GameState.GameOver:
                    inputHandler.SetEnabled(false);
                    inputHandler.SetEnabledPauseButton(false);
                    OnGameOverEvent.Invoke();
                    break;
                case GameState.GameWon:
                    inputHandler.SetEnabled(false);
                    inputHandler.SetEnabledPauseButton(false);
                    OnGameWonEvent.Invoke();
                    break;
                case GameState.Playing:
                    inputHandler.SetEnabled(true);
                    inputHandler.SetEnabledPauseButton(true);
                    OnGamePlayingEvent.Invoke();
                    break;
                case GameState.Paused:
                    inputHandler.SetEnabled(false);
                    inputHandler.SetEnabledPauseButton(true);
                    OnGamePausedEvent.Invoke();
                    break;
                case GameState.BeforePlay:
                    inputHandler.SetEnabled(false);
                    inputHandler.SetEnabledPauseButton(false);
                    OnGameBeforePlayEvent.Invoke();
                    break;
            }
        }

        public bool CompareState(GameState state)
        {
            return currentState == state;
        }
    }
}