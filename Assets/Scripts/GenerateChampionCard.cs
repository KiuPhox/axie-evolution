using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

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

    [Header("Lock Button")]
    public Image lockImage;
    public TMP_Text lockText;
    public Sprite lockSprite;
    public Sprite unlockSprite;
    bool isLocked = false;

    List<Level.Item> itemsGenerated = new List<Level.Item>();

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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateCard();
        }
#endif
    }

    public void GenerateCard()
    {
        if (!isLocked)
        {
            SoundManager.Instance.PlaySound(flipClip);

            CalculateWeights();

            itemsGenerated = new List<Level.Item>();

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].gameObject.SetActive(true);
                cards[i].transform.localScale = cards[i].originalScale;

                if (GameManager.Instance.State == GameState.ChooseItem)
                { 
                    var randomItem = Level.Items.ElementAt(Random.Range(0, Level.Items.Count));
                    while (itemsGenerated.Contains(randomItem))
                    {
                        randomItem = Level.Items.ElementAt(Random.Range(0, Level.Items.Count));
                    }
                    itemsGenerated.Add(randomItem);
                    cards[i].SetCardItem(randomItem);
                }
                else
                {
                    randomChampion = champions[GetRandomChampionIndex()];
                    cards[i].SetCardChampionData(randomChampion);
                }
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
        int moneyReroll = 2;
        if (GameManager.Instance.State == GameState.ChooseItem)
            moneyReroll = GameManager.Instance.currentLevel - 1;
        if (!isLocked && moneyUI.startingMoney >= moneyReroll)
        {
            moneyUI.startingMoney -= moneyReroll;
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
            lockImage.sprite = unlockSprite;
        }
        else
        {
            lockText.text = "Lock";
            lockImage.sprite = lockSprite;
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
