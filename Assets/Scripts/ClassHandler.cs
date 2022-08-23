using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClassHandler : MonoBehaviour
{
    string beast = "<color=#FAE75A>Beast Effect:</color> Increase 30% defense for all axies";
    string aquatic = "<color=#FAE75A>Aquatic Effect:</color> Slow enemies for 30%";
    string bug = "<color=#FAE75A>Bug Effect:</color> Increase 30% damage for Bug class axie";
    string bird = "<color=#FAE75A>Bird Effect:</color> Descrease 30% attack speed for Bird class axie";
    string plant = "<color=#FAE75A>Plant Effect:</color> Increase 25% max health for all axies";
    string reptile = "<color=#FAE75A>Reptile Effect:</color> Ignore enemies sheild";

    public GameObject classDescriptionUI;
    public TMP_Text descriptionText;

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

    }

    public void DisableDescriptionClass()
    {
        classDescriptionUI.SetActive(false);
    }
}
