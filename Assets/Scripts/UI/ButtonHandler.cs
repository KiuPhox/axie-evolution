using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour
{
    public void ClickButton()
    {
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.05f).SetLoops(2, LoopType.Yoyo);
    }
    public void ScaleButton()
    {
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.05f);
    }

    public void OriginalScale()
    {
        transform.DOScale(new Vector2(1f, 1f), 0.05f);
    }

    public void DoneSelected()
    {
        GameManager.Instance.UpdateGameState(GameState.GameStart);
    }

    public void Play()
    {
        GameManager.Instance.UpdateGameState(GameState.ChooseCard);
    }
}
