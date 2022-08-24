using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemsUI : MonoBehaviour
{
    public PlayerChampions playerChampions;
    public GameObject[] units;

    void Start()
    {
        DisableAllItems();
    }

    private void DisableAllItems()
    {
        foreach (GameObject unit in units)
        {
            unit.SetActive(false);
        }
    }

    public void SetItemToUnit()
    {
        DisableAllItems();
        var playerItems = playerChampions.playerItems;
        int index = 0;
        foreach (var playerItem in playerItems)
        {
            LoadItemUnit(units[index], playerItem);
            index++;
        }
    }

    public void LoadItemUnit(GameObject unit, string playerItem)
    {
        unit.SetActive(true);
        unit.name = playerItem;
        TMP_Text item_text = unit.GetComponentInChildren<TMP_Text>();
        item_text.text = playerItem;
    }
}
