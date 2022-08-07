using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChampions : MonoBehaviour
{
    [HideInInspector] public GameObject choosedChampion;
    [HideInInspector] public List<GameObject> champions = new List<GameObject>();

    public int championsCount;

    Vector3 offset = new Vector3(-0.75f, 0f, 0f);
    Vector3 shadowOffset = new Vector3(0, -0.6f, 0f);

    public void Update()
    {
        
    }

    public void AddChampion(GameObject choosedChampion)
    {
        GameObject insChampion;
        insChampion = Instantiate(choosedChampion, transform);
        insChampion.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        champions.Add(insChampion);
        UpdateChampions(champions.Count);
    }

    public bool CheckExistedChampion(GameObject choosedChampion)
    {
        string choosedChampionName = choosedChampion.GetComponent<Champion>().championData.name;
        foreach (GameObject champion in champions)
        {
            if (choosedChampionName == champion.GetComponent<Champion>().championData.name)
            {
                SetReserveChampion(champion);
                return true;
            }
        }
        return false;
    }

    public int GetChampionLevel(GameObject choosedChampion)
    {
        string choosedChampionName = choosedChampion.GetComponent<Champion>().championData.name;
        foreach (GameObject champion in champions)
        {
            if (choosedChampionName == champion.GetComponent<Champion>().championData.name)
            {
                return champion.GetComponent<Champion>().currentLevel;
            }
        }
        return -1;
    }

    void SetReserveChampion(GameObject champion)
    {
        Champion c = champion.GetComponent<Champion>();
        if (c.currentLevel == 1)
        {
            c.reserve[1]++;
            if (c.reserve[1] > 1)
            {
                c.reserve[1] = 0;
                c.currentLevel = 2;
                c.BuffLevel();
            }
        }
        else if (c.currentLevel == 2)
        {
            c.reserve[1]++;
            if (c.reserve[1] > 2)
            {
                if (c.reserve[2] == 1)
                {
                    c.reserve[2] = 0;
                    c.reserve[1] = 0;
                    c.currentLevel = 3;
                    c.BuffLevel();
                }
                else
                {
                    c.reserve[2]++;
                    c.reserve[1] = 0;
                }
            }
        }
    }

    public void ResetAllChampions()
    {
        foreach (GameObject champion in champions)
        {
            champion.SetActive(true);
            champion.GetComponent<Champion>().SetCharacteristics();
        }
    }

    public void UpgradeChampion(GameObject champion)
    {
        champion.GetComponent<LivingEntity>().currentLevel++;
    }

    public void RemoveChampion(GameObject choosedChampion)
    {
        champions.Remove(choosedChampion);
        UpdateChampions(champions.Count);
    }

    public void UpdateChampions(int count)
    {
        championsCount = count;
    }
}
