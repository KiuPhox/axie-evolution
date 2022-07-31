using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardHandler : MonoBehaviour
{
    public PlayerChampions pc;
    public Image _image;
    public TMP_Text _name;
    public TMP_Text _description;
    public TMP_Text _damage;
    public TMP_Text _defense;
    public TMP_Text _tier;

    RectTransform cardTransform;

    public void SelectChampion()
    {   
        GameObject choosedChampion = Resources.Load("Prefabs/" + _name.text) as GameObject;
        pc.AddChampion(choosedChampion);
    }

    public void SetCardData(ChampionData champion)
    {
        _image.sprite = Resources.Load<Sprite>("Sprite/" + champion.name);
        LoadChampionImage(_image);
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

    private void Start()
    {
        cardTransform = GetComponent<RectTransform>();
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
