using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitsUI : MonoBehaviour
{
    public PlayerChampions playerChampions;
    public GameObject[] units;
    
    void Start()
    {
        foreach (GameObject unit in units)
        {
            unit.SetActive(false);
        }
    }

    public void SetChampionsToUnit()
    {
        for (int i = 0; i < playerChampions.champions.Count; i++)
        {
            LoadChampionUnit(units[i], playerChampions.champions[i]);
        }
    }


    public void LoadChampionUnit(GameObject unit, GameObject champion)
    {
        unit.SetActive(true);
        Image[] images = unit.GetComponentsInChildren<Image>();
        
        // Load Champion Image
        images[0].sprite = Resources.Load<Sprite>("Sprite/" + champion.name.Replace("(Clone)", ""));
        images[0].SetNativeSize();
        RectTransform imageRT = images[0].GetComponent<RectTransform>();
        Vector2 originalSize = imageRT.sizeDelta;
        imageRT.sizeDelta = new Vector2(50 * originalSize.x / originalSize.y, 50);
        imageRT.localPosition = new Vector2(imageRT.localPosition.x, 10f);

        // Load Champion Level
        images[1].sprite = Resources.Load<Sprite>("Sprite/" + champion.GetComponent<Champion>().currentLevel + "star");
    }
}
