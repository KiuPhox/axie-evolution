using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemHandler : MonoBehaviour
{
    public GameObject classDescriptionUI;
    public TMP_Text descriptionText;

    public void ShowDescriptionItem()
    {
        classDescriptionUI.SetActive(true);
        foreach (var item in Level.DescriptionItems)
        {
            if (item.name == gameObject.name)
            {
                descriptionText.text = item.description;
                break;
            }
        }
    }

    public void DisableDescriptionClass()
    {
        classDescriptionUI.SetActive(false);
    }
}
