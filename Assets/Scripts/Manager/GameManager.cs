using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;
using AxieMixer.Unity;
public class GameManager : MonoBehaviour
{
    public int currentLevel = 1;

    public static GameManager Instance;
    public GameState State { get; private set; }
    public GameState previousState { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    public PlayerChampions playerChampions;
    public GenerateChampionCard generateChampionCard;
    public Spawner spawner;

    private void Awake()
    {
        Mixer.Init();
        Instance = this;
    }

    void Start()
    {
        DOTween.SetTweensCapacity(500, 150);
        UpdateGameState(GameState.Idle);
        if (currentLevel != 1 && currentLevel % 5 == 1)
        {
            UpdateGameState(GameState.ChooseItem);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        previousState = State;
        State = newState;

        // Debug.Log("pre: " + previousState);
        // Debug.Log("current: " + State);

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
            case GameState.ChooseItem:
                HandleChooseItem();
                break;
            case GameState.GameVictory:
                HandleGameVictory();
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
        if (previousState == GameState.ChooseCard)
        {
            spawner.isStarted = false;
        }
        playerChampions.SetMutiplierValues();
    }

    private void HandleGamePause()
    {
        Time.timeScale = 0f;
    }

    private void HandleGameOver()
    {
        
    }

    private void HandleGameVictory()
    {

    }

    private void HandleChooseCard()
    {
        Time.timeScale = 1f;
        playerChampions.ResetAllChampions();
        if (previousState != GameState.GamePause && generateChampionCard.isFisrtGenerated)
        {
            if (previousState != GameState.ChooseItem)
            {
                currentLevel++;
            }
            generateChampionCard.EarnMoney();
            generateChampionCard.GenerateCard();
        }
    }

    private void HandleChooseItem()
    {
        Time.timeScale = 1f;
        playerChampions.ResetAllChampions();
        if (previousState != GameState.GamePause && generateChampionCard.isFisrtGenerated)
        {
            currentLevel++;
            generateChampionCard.EarnMoney();
            generateChampionCard.GenerateCard();
        }
    }
}

public enum GameState
{
    Idle,   
    GameStart,
    ChooseCard,
    ChooseItem,
    GamePause,
    GameOver,
    GameVictory
}
