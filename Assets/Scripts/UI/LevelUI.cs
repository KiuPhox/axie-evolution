using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class LevelUI : MonoBehaviour
{
    public TMP_Text levelText;
    bool isShowed = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.ChooseCard)
        {
            isShowed = false;
        }
        if (!isShowed && GameManager.Instance.previousState == GameState.ChooseCard && GameManager.Instance.State == GameState.GameStart)
        {
            string level = GameManager.Instance.currentLevel.ToString();
            transform.DOLocalMoveY(0f, 0.8f);
            levelText.DOFade(1, 0.8f).OnComplete(() =>
            {
                transform.DOLocalMoveY(50f, 0.8f).SetDelay(0.5f).OnComplete(() =>
                {
                    transform.DOLocalMoveY(-50f, 0f);
                });
                levelText.DOText("Level " + level + "/25", 0.3f);
                levelText.DOFade(0, 0.8f).SetDelay(0.5f);
            });
            
            isShowed = true;
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
        levelText.DOKill();
    }
}
