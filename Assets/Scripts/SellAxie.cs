using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellAxie : MonoBehaviour, IPointerClickHandler
{
    PlayerChampions playerChampions;

    public void Start()
    {
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.State == GameState.ChooseCard && eventData.button == PointerEventData.InputButton.Right)
        {
            int championIndex = int.Parse(name.Replace("Unit ", ""));
            playerChampions.RemoveChampion(championIndex);
        }
    }
}
