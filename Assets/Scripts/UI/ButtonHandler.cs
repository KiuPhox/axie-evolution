using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour
{
    public void ScaleButton()
    {
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.05f).SetLoops(2, LoopType.Yoyo);
    }

    public void DoneSelected()
    {
        GameManager.Instance.UpdateGameState(GameState.GameStart);
    }
}
