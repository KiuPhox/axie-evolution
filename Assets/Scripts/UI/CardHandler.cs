using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardHandler : MonoBehaviour
{
    public PlayerChampions pc;
    public Image _image;
    public TMP_Text _name;
    public TMP_Text _description;
    public TMP_Text _damage;
    public TMP_Text _defense;
    public TMP_Text _tier;

    public void SelectChampion()
    {   
        GameObject choosedChampion = Resources.Load("Prefabs/" + _name.text) as GameObject;
        pc.AddChampion(choosedChampion);
    }

    public void SetCardData(ChampionData champion)
    {
        _image.sprite = Resources.Load<Sprite>("Sprite/" + champion.name);
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
}
