using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GenerateChampionCard : MonoBehaviour
{
    [HideInInspector] public ChampionData[] champions;
    public float[] tierChances;

    public CardHandler[] cards;
 
    float accumlateWeights;
    ChampionData randomChampion;
    AudioSource flipSound;

    public MoneyUI moneyUI;

    // Start is called before the first frame update

    private void Awake()
    {
        champions = Resources.LoadAll("Champion Data", typeof(ChampionData)).Cast<ChampionData>().ToArray();
    }

    void Start()
    {
        CalculateWeights();
        flipSound = GetComponent<AudioSource>();
        Invoke("GenerateCard", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateCard();
        }
    }

    public void GenerateCard()
    {
        flipSound.Play();
        

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
        accumlateWeights = 0f;
        for (int i = 0; i < champions.Length; i++)
        {
            accumlateWeights += tierChances[champions[i].tier - 1];
            champions[i].weight = accumlateWeights;
        }
    }
}
