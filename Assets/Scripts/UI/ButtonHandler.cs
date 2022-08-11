using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class ButtonHandler : MonoBehaviour
{
    public void ClickButton()
    {
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.05f).SetLoops(2, LoopType.Yoyo);
    }
    public void ScaleButton()
    {
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.05f).SetUpdate(true);
    }

    public void OriginalScale()
    {
        transform.DOScale(new Vector2(1f, 1f), 0.05f).SetUpdate(true);
    }

    public void DoneSelected()
    {
        PlayerChampions pc = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
        if (pc.champions.Count > 0)
        {
            GameManager.Instance.UpdateGameState(GameState.GameStart);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        GameManager.Instance.TriggerPause();
    }

    public void Play()
    {
        GameManager.Instance.UpdateGameState(GameState.ChooseCard);
    }
}
