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
    public GameState previousState { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    public GenerateChampionCard generateChampionCard;

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
        previousState = State;
        State = newState;

        Debug.Log("pre: " + previousState);
        Debug.Log("current: " + State);

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



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TriggerPause();
        }
    }

    public void TriggerPause()
    {
        if (State == GameState.GamePause)
        {
            UpdateGameState(previousState);
        }
        else
        {
            UpdateGameState(GameState.GamePause);
        }
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
        Time.timeScale = 1f;
        if (previousState != GameState.GamePause && generateChampionCard.isFisrtGenerated)
        {
            generateChampionCard.GenerateCard();
        }
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
