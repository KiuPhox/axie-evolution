using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UnitsUI : MonoBehaviour
{
    public PlayerChampions playerChampions;
    public GameObject[] units;

    bool isResetHealth = false;
    void Start()
    {
        foreach (GameObject unit in units)
        {
            unit.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.State == GameState.ChooseCard && !isResetHealth)
        {
            isResetHealth = true;
            ResetAllHealthBar();
        }
        if (GameManager.Instance.State == GameState.GameStart)
        {
            isResetHealth = false;
        }
    }

    public void SetChampionsToUnit()
    {
        for (int i = 0; i < playerChampions.champions.Count; i++)
        {
            LoadChampionUnit(units[i], playerChampions.champions[i]);
        }
        for (int i = playerChampions.champions.Count; i < 7; i++)
        {
            units[i].SetActive(false);
        }
    }

    public void ResetAllHealthBar()
    {
        for (int i = 0; i < playerChampions.champions.Count; i++)
        {
            Image[] images = units[i].GetComponentsInChildren<Image>();
            images[3].fillAmount = 1;
        }
    }
    public void LoadHealthbar(GameObject champion, float health, float maxHealth)
    {
        for (int i = 0; i < playerChampions.champions.Count; i++)
        {
            if (champion == playerChampions.champions[i])
            {
                Image[] images = units[i].GetComponentsInChildren<Image>();
                images[3].fillAmount = health / maxHealth;
                return;
            }
        }
    }

    public void LoadChampionUnit(GameObject unit, GameObject champion)
    {
        unit.SetActive(true);
        Image[] images = unit.GetComponentsInChildren<Image>();
        TMP_Text unitRemaning_text = unit.GetComponentInChildren<TMP_Text>();
        

        // Load Champion Image
        images[0].sprite = champion.GetComponent<Champion>().championData.sprite;
        images[0].SetNativeSize();
        RectTransform imageRT = images[0].GetComponent<RectTransform>();
        Vector2 originalSize = imageRT.sizeDelta;
        imageRT.sizeDelta = new Vector2(50 * originalSize.x / originalSize.y, 50);
        imageRT.localPosition = new Vector2(imageRT.localPosition.x, 10f);

        // Load Champion Level
        int level = champion.GetComponent<Champion>().currentLevel;
        images[1].sprite = Resources.Load<Sprite>("Sprites/" + level + "star");

        // Load Remaning Units
        int unitRemaning;
        int[] reserve = champion.GetComponent<Champion>().reserve;
        if (level == 1)
        {
            unitRemaning = 2 - reserve[1];
            unitRemaning_text.text = unitRemaning.ToString();
        }
        else if (level == 2)
        {
            unitRemaning = 6 - reserve[1] - 3 * reserve[2];
            unitRemaning_text.text = unitRemaning.ToString();
        }
        if (level == 3)
        {
            unitRemaning_text.text = "";
        }
    }
}
