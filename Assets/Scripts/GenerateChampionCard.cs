using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class GenerateChampionCard : MonoBehaviour
{
    [HideInInspector] public bool isFisrtGenerated;
    [HideInInspector] public ChampionData[] champions;
    [SerializeField] AudioClip flipClip;

    public float[] tierChances;

    public CardHandler[] cards;

    CanvasManager canvasManager;

    float accumlateWeights;
    ChampionData randomChampion;
   
    public MoneyUI moneyUI;
    public TMP_Text lockText;

    bool isLocked = false;
    
    private void Awake()
    {
        champions = Resources.LoadAll("Champion Data", typeof(ChampionData)).Cast<ChampionData>().ToArray();
        isFisrtGenerated = false;
    }

    void Start()
    {
        CalculateWeights();
        canvasManager = GetComponentInParent<CanvasManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFisrtGenerated && canvasManager.chooseCardUI.activeSelf == true)
        {
            EarnMoney();
            isFisrtGenerated = true;
            GenerateCard();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateCard();
        }
    }

    public void GenerateCard()
    {
        if (!isLocked)
        {
            SoundManager.Instance.PlaySound(flipClip);

            CalculateWeights();

            for (int i = 0; i < cards.Length; i++)
            {
                randomChampion = champions[GetRandomChampionIndex()];
                Debug.Log(randomChampion.name);
                cards[i].gameObject.SetActive(true);
                cards[i].transform.localScale = cards[i].originalScale;
                cards[i].SetCardData(randomChampion);
            }
        }
    }

    public void EarnMoney()
    {
        Vector2 moneyRange = Level.levelToMoneyRange[GameManager.Instance.currentLevel - 1];
        moneyUI.startingMoney += Random.Range(Mathf.RoundToInt(moneyRange.x), Mathf.RoundToInt(moneyRange.y));
        moneyUI.isChanged = true;
    }

    public void Reroll()
    {
        if (!isLocked && moneyUI.startingMoney >= 2)
        {
            moneyUI.startingMoney -= 2;
            moneyUI.isChanged = true;
            GenerateCard();
        }
    }

    public void TriggerLock()
    {
        isLocked = !isLocked;
        if (isLocked)
        {
            lockText.text = "Unlock";
        }
        else
        {
            lockText.text = "Lock";
        }
    }

    private int GetRandomChampionIndex()
    {
        float r = Random.Range(0f, 1f) * accumlateWeights;
        for (int i = 0; i < champions.Length; i++)
        {
            if (champions[i].weight >= r)
            {
                return i;
            }
        }
        return 0;
    }

    void CalculateWeights()
    {
        tierChances = Level.levelToTierChances[GameManager.Instance.currentLevel - 1];
        accumlateWeights = 0f;
        for (int i = 0; i < champions.Length; i++)
        {
            accumlateWeights += tierChances[champions[i].tier - 1];
            champions[i].weight = accumlateWeights;
        }
    }
}
