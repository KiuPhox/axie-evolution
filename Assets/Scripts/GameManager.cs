using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State { get; private set; }
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Idle);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Idle:
                HandleIdle();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            case GameState.GameStart:
                HandleGameStart();
                break;
            case GameState.GamePause:
                HandleGamePause();
                break;
            case GameState.ChooseCard:
                HandleChooseCard();
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }



    // Update is called once per frame
    void Update()
    {

    }

    private void HandleIdle()
    {
        
    }

    private void HandleGameStart()
    {
        Time.timeScale = 1f;
    }

    private void HandleGamePause()
    {
        Time.timeScale = 0f;
    }

    private void HandleGameOver()
    {
        
    }
    
    private void HandleChooseCard()
    {

    }
}

public enum GameState
{
    Idle,   
    GameStart,
    ChooseCard,
    GamePause,
    GameOver,
}
