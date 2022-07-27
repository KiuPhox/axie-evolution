using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChampions : MonoBehaviour
{
    [HideInInspector] public GameObject choosedChampion;
    [HideInInspector] public List<GameObject> champions = new List<GameObject>();

    public int championsCount;
    
    Vector3 offset = new Vector3(-0.75f, 0f, 0f);

    public void Update()
    {
        
    }

    public void AddChampion(GameObject choosedChampion)
    {
        GameObject insChampion;
        insChampion = Instantiate(choosedChampion, transform);

        if (champions.Count == 0)
        {
            insChampion.transform.position = transform.position;
        }
        else
        {
            insChampion.transform.position = champions[champions.Count - 1].transform.position + offset;
        }
        champions.Add(insChampion);
        UpdateChampions(champions.Count);
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
