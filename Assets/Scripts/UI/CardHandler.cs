using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardHandler : MonoBehaviour
{
    public PlayerChampions pc;
    public TMP_Text _name;
    public TMP_Text _description;
    public TMP_Text _damage;
    public TMP_Text _defense;
    public TMP_Text _tier;

    public MoneyUI moneyUI;

    Image[] images;
    private void Start()
    {
        images = GetComponentsInChildren<Image>();
    }

    public void SelectChampion()
    {   
        GameObject choosedChampion = Resources.Load("Prefabs/" + _name.text) as GameObject;

        if (moneyUI.startingMoney >= int.Parse(_tier.text))
        {
            pc.AddChampion(choosedChampion);
            moneyUI.startingMoney -= int.Parse(_tier.text);
            moneyUI.isChanged = true;


            gameObject.SetActive(false);   
        }
    }

    public void SetCardData(ChampionData champion)
    {
        CardColorData cardColorData = champion.cardColor;

        // Load Card Color Data
        images[0].color = cardColorData.color; // Body's color
        images[1].color = cardColorData.championBoxColor; // Champion Box's color
        images[2].sprite = Resources.Load<Sprite>("Sprite/" + champion.name); // Champion's image
        LoadChampionImage(images[2]);
        images[3].color = cardColorData.nameBoxColor; // Name Box's color
        images[4].color = cardColorData.descriptionBoxColor; // Name Box's color

        // Load Champion Data
        _name.text = champion.name;
        _description.text = champion.description;
        _defense.text = champion.defense.ToString();
        _damage.text = champion.damage.ToString();
        _tier.text = champion.tier.ToString();
    }

    public void DoneSelected()
    {
        GameManager.Instance.UpdateGameState(GameState.GameStart);
    }

    private void LoadChampionImage(Image image)
    {
        image.SetNativeSize();
        RectTransform imageRT = image.GetComponent<RectTransform>();
        Vector2 originalSize = imageRT.sizeDelta;
        imageRT.sizeDelta = new Vector2(100 * originalSize.x / originalSize.y, 100);
    }



    private void Update()
    {

    }
    public void ScaleBiggerCard()
    {
        transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutCubic);
    }
    public void ScaleSmallerCard()
    {
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutCubic);
    }
}
