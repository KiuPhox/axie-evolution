using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GenerateChampionCard : MonoBehaviour
{
    public ChampionData[] champions;
    public float[] tierChances;

    public TMP_Text[] cardNames;
 
    float accumlateWeights;
    // Start is called before the first frame update
    void Start()
    {
        CalculateWeights();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < cardNames.Length; i++)
            {
                cardNames[i].SetText(champions[GetRandomChampionIndex()].name);
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
