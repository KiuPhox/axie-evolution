﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    public GameObject moneyBoxUI;
    public GameObject idleMenuUI;
    public GameObject chooseCardUI;
    public GameObject pauseMenuUI;
    public GameObject waveCountUI;
    public GameObject unitsHolderUI;
    public GameObject gameOverUI;
    public GameObject classesHolderUI;
    public GameObject itemsHolderUI;
    public GameObject rerollButtonUI;
    public GameObject acceptButtonUI;
    public GameObject pauseButtonUI;
    public GameObject bossHealthBarUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.ChooseCard || GameManager.Instance.State == GameState.ChooseItem)
        {
            chooseCardUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            idleMenuUI.SetActive(false);
            moneyBoxUI.SetActive(true);
            waveCountUI.SetActive(false);
            unitsHolderUI.SetActive(true);
            classesHolderUI.SetActive(true);
            itemsHolderUI.SetActive(true);
            pauseButtonUI.SetActive(true);
            bossHealthBarUI.SetActive(false);
            if (GameManager.Instance.State == GameState.ChooseCard)
            {
                acceptButtonUI.SetActive(true);
                rerollButtonUI.GetComponentInChildren<TMP_Text>().text = "Reroll: 2";
            }
            else if (GameManager.Instance.State == GameState.ChooseItem)
            {
                rerollButtonUI.GetComponentInChildren<TMP_Text>().text = "Reroll: " + (GameManager.Instance.currentLevel - 1).ToString();
                acceptButtonUI.SetActive(false);
            }
        }
        else if (GameManager.Instance.State == GameState.GamePause)
        {
            chooseCardUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            moneyBoxUI.SetActive(false);
            waveCountUI.SetActive(false);
            unitsHolderUI.SetActive(false);
            idleMenuUI.SetActive(false);
            classesHolderUI.SetActive(true);
            itemsHolderUI.SetActive(true);
            pauseButtonUI.SetActive(true);
        }
        else if (GameManager.Instance.State == GameState.GameStart)
        {
            chooseCardUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            moneyBoxUI.SetActive(false);
            waveCountUI.SetActive(true);
            unitsHolderUI.SetActive(true);
            classesHolderUI.SetActive(false);
            itemsHolderUI.SetActive(false);
            pauseButtonUI.SetActive(true);
            if (GameManager.Instance.currentLevel % 5 == 0)
            {
                bossHealthBarUI.SetActive(true);
            }
            else
            {
                bossHealthBarUI.SetActive(false);
            }
        }
        else if (GameManager.Instance.State == GameState.GameOver || GameManager.Instance.State == GameState.GameVictory)
        {
            gameOverUI.SetActive(true);
        }
        else if (GameManager.Instance.State == GameState.Idle)
        {
            pauseMenuUI.SetActive(false);
            idleMenuUI.SetActive(true);
            pauseButtonUI.SetActive(false);
        }
    }
}
