using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClassesUI : MonoBehaviour
{
    public PlayerChampions playerChampions;
    public GameObject[] units;
    void Start()
    {
        DisableAllClasses();
    }

    private void DisableAllClasses()
    {
        foreach (GameObject unit in units)
        {
            unit.SetActive(false);
        }
    }

    public void SetClassToUnit()
    {
        DisableAllClasses();
        var playerClasses = playerChampions.playerClasses;
        int index = 0;
        foreach (var playerClass in playerClasses)
        {
            if (playerClass.value > 0)
            {
                LoadClassUnit(units[index], playerClass);
                index++;
            }
        }
        
    }

    public void LoadClassUnit(GameObject unit, PlayerChampions.PlayerClass playerClass)
    {
        unit.SetActive(true);
        Image[] images = unit.GetComponentsInChildren<Image>();
        TMP_Text class_text = unit.GetComponentInChildren<TMP_Text>();

        // Load Class Image
        images[1].sprite = Resources.Load<Sprite>("Sprites/" + playerClass._class.ToString().ToLower());
        // Load Class Value
        class_text.text = playerClass.value + "/2";
    }
}
