using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerChampions : MonoBehaviour
{
    [HideInInspector] public GameObject choosedChampion;
    [HideInInspector] public List<GameObject> champions = new List<GameObject>();
    [SerializeField] UnitsUI unitsUI;
    [SerializeField] ClassesUI classesUI;
    [SerializeField] MoneyUI moneyUI;

    [TableList]
    public List<PlayerClass> playerClasses = new List<PlayerClass>();
    
    // Multipliers
    [HideInInspector] public float[] squirl_m;
    [HideInInspector] public float beastDfs_m;
    [HideInInspector] public float aquaticSlow_m;
    [HideInInspector] public float birdCooldown_m;
    [HideInInspector] public float plantHealth_m;
    [HideInInspector] public float bugDmg_m;
    public void AddChampion(GameObject choosedChampion)
    {
        GameObject insChampion;
        insChampion = Instantiate(choosedChampion, transform);
        insChampion.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        champions.Add(insChampion);
        SetMutiplierValues();
    }

    private void SetMutiplierValues()
    {
        squirl_m = new float[] { 1, 1, 1 };
        beastDfs_m = 1;
        aquaticSlow_m = 1;
        birdCooldown_m = 1;
        plantHealth_m = 1;
        bugDmg_m = 1;

        foreach (PlayerClass playerClass in playerClasses)
        {
            playerClass.value = 0;
            playerClass.isActive = false;
        }

        foreach (GameObject championGO in champions)
        {
            Champion champion = championGO.GetComponent<Champion>();
            
            foreach (Class @class in champion.championData.classes)
            {
                foreach (PlayerClass playerClass in playerClasses)
                {
                    if (playerClass._class == @class)
                    {
                        playerClass.value++;
                    }

                    if (playerClass.value >= 3)
                    {
                        playerClass.isActive = true;
                        switch (playerClass._class)
                        {
                            case Class.Beast:
                                beastDfs_m = 1.3f;
                                break;
                            case Class.Bird:
                                birdCooldown_m = 0.7f;
                                break;
                            case Class.Aquatic:
                                aquaticSlow_m = 0.7f;
                                break;
                            case Class.Plant:
                                plantHealth_m = 1.25f;
                                break;
                            case Class.Bug:
                                bugDmg_m = 1.25f;
                                break;
                        }
                    }
                }
            }

            if (champion.championData.name == "Squirl")
            {
                squirl_m[0] = 1.2f;
                squirl_m[1] = 1.2f;
                if (champion.currentLevel == 3)
                {
                    squirl_m[0] = 1.5f;
                    squirl_m[1] = 1.5f;
                    squirl_m[2] = 0.8f;
                }
                continue;
            }
        }

        ResetAllChampions();
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
        SetMutiplierValues();
    }

    public void Update()
    {
        if (GameManager.Instance.State == GameState.GameStart)
        {
            int count = 0;
            foreach (GameObject champion in champions)
            {
                if (champion.activeSelf)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                GameManager.Instance.UpdateGameState(GameState.GameOver);
                Debug.Log("Game Over");
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

    public void RemoveChampion(int championIndex)
    {
        Champion c = champions[championIndex].GetComponent<Champion>();
        int starMoney = 1;
        if (c.currentLevel == 2)
        {
            starMoney = 3;
        }
        else if (c.currentLevel == 3)
        {
            starMoney = 9;
        }
        moneyUI.startingMoney += (starMoney + c.reserve[1] + c.reserve[2] * 3) * c.championData.tier;
        moneyUI.isChanged = true;
        Destroy(champions[championIndex]);
        champions.RemoveAt(championIndex);
        SetMutiplierValues();
        unitsUI.SetChampionsToUnit();
        classesUI.SetClassToUnit();
    }

    [System.Serializable]
    public class PlayerClass
    {
        public Class _class;
        public int value;
        public bool isActive;
    };
}
