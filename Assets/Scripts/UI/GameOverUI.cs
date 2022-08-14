using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    public RectTransform rectTransform;
    public TMP_Text gameOverText;
    void Start()
    {
        rectTransform.DOLocalMoveY(75f, 1f).SetEase(Ease.OutBounce);
        if (GameManager.Instance.State == GameState.GameVictory)
        {
            gameOverText.text = "Victory";
        }
    }

    private void OnDestroy()
    {
        rectTransform.DOKill();
    }
}
