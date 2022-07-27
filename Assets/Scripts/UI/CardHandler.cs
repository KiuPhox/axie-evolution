using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardHandler : MonoBehaviour
{
    public PlayerChampions pc;
    TMP_Text cardName;
    public void SelectChampion()
    {
        cardName = GetComponentInChildren<TMP_Text>();
        string championName = cardName.text;
        GameObject choosedChampion = Resources.Load("Prefabs/" + championName) as GameObject;
        pc.AddChampion(choosedChampion);
    }
}
