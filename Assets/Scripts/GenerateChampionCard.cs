using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
        SoundManager.Instance.PlaySound(flipClip);

        CalculateWeights();

        for (int i = 0; i < cards.Length; i++)
        {
            randomChampion = champions[GetRandomChampionIndex()];
            Debug.Log(randomChampion.name);
            cards[i].gameObject.SetActive(true);
            cards[i].transform.localScale = new Vector2(1f, 1f);
            cards[i].SetCardData(randomChampion);
        }
    }

    public void Reroll()
    {
        if (moneyUI.startingMoney >= 2)
        {
            moneyUI.startingMoney -= 2;
            moneyUI.isChanged = true;
            GenerateCard();
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
        // Fix
        tierChances = Level.levelToTierChances[GameManager.Instance.currentLevel - 1];
        accumlateWeights = 0f;
        for (int i = 0; i < champions.Length; i++)
        {
            accumlateWeights += tierChances[champions[i].tier - 1];
            champions[i].weight = accumlateWeights;
        }
    }
}
