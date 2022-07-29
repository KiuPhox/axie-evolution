using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class GenerateChampionCard : MonoBehaviour
{
    [HideInInspector] public ChampionData[] champions;
    public float[] tierChances;

    public CardHandler[] cards;
 
    float accumlateWeights;
    ChampionData randomChampion;
    // Start is called before the first frame update

    private void Awake()
    {
        champions = Resources.LoadAll("Champion Data", typeof(ChampionData)).Cast<ChampionData>().ToArray();
    }

    void Start()
    {
        CalculateWeights();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < cards.Length; i++)
            {
                randomChampion = champions[GetRandomChampionIndex()];
                Debug.Log(randomChampion.name);
                cards[i].SetCardData(randomChampion);
            }
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
