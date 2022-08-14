using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    public RectTransform rectTransform;
    void Start()
    {
        rectTransform.DOLocalMoveY(75f, 1f).SetEase(Ease.OutBounce);
    }

    private void OnDestroy()
    {
        rectTransform.DOKill();
    }
}
