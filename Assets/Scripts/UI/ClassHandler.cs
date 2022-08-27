using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClassHandler : MonoBehaviour
{
    string beast = "<color=#FAE75A>Beast Effect:</color> Increase 30% defense for all axies";
    string aquatic = "<color=#2FD4FD>Aquatic Effect:</color> Slow enemies for 15%";
    string bug = "<color=#FD2423>Bug Effect:</color> Increase 30% damage for Bug class axie";
    string bird = "<color=#F64FA9>Bird Effect:</color> Descrease 30% attack speed for Bird class axie";
    string plant = "<color=#5FE722>Plant Effect:</color> Increase 25% max health for all axies";
    string reptile = "<color=#9B5CCC>Reptile Effect:</color> Ignore enemies sheild";

    public GameObject classDescriptionUI;
    public TMP_Text descriptionText;

    PlayerChampions playerChampions;
    private void Awake()
    {
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
    }

    public void ShowDescriptionClass()
    {
        classDescriptionUI.SetActive(true);
        string description = "";
        switch (gameObject.name)
        {
            case "Beast":
                description = beast;
                break;
            case "Aquatic":
                description = aquatic;
                break;
            case "Plant":
                description = plant;
                break;
            case "Bird":
                description = bird;
                break;
            case "Bug":
                description = bug;
                break;
            case "Reptile":
                description = reptile;
                break;
        }
        descriptionText.text = description;

        foreach(GameObject championGO in playerChampions.champions)
        {
            List<Class> classes = championGO.GetComponent<Champion>().championData.classes;
            foreach (var @class in classes)
            {
                if (@class.ToString() == gameObject.name)
                {
                    Debug.Log(championGO.name);
                }
            }
        }
    }

    public void DisableDescriptionClass()
    {
        classDescriptionUI.SetActive(false);
    }
}
