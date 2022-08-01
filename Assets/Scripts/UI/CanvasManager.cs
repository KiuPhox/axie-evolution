using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject moneyBoxUI;
    public GameObject idleMenuUI;
    public GameObject chooseCardUI;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.ChooseCard)
        {
            chooseCardUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            idleMenuUI.SetActive(false);
            moneyBoxUI.SetActive(true);
        }
        else if (GameManager.Instance.State == GameState.GamePause)
        {
            chooseCardUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            moneyBoxUI.SetActive(false);
        }
        else if (GameManager.Instance.State == GameState.GameStart)
        {
            chooseCardUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            moneyBoxUI.SetActive(false);
        }
    }
}
