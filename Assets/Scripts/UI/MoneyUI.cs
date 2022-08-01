using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyUI : MonoBehaviour
{
    public int startingMoney;
    public TMP_Text moneyText;

    [HideInInspector] public bool isChanged = false;
    [SerializeField] AudioClip coinClip;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = startingMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChanged)
        {
            isChanged = false;
            moneyText.text = startingMoney.ToString();
            SoundManager.Instance.PlaySound(coinClip);
        }
    }
}
