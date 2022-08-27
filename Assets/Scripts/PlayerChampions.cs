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

    public List<string> playerItems = new List<string>();

    // Champion Multipliers
    [HideInInspector] public float[] squirl_m;

    // Class Multipliers
    [HideInInspector] public float beastDfs_m;
    [HideInInspector] public float aquaticSlow_m;
    [HideInInspector] public float birdCooldown_m;
    [HideInInspector] public float plantHealth_m;
    [HideInInspector] public float bugDmg_m;
    [HideInInspector] public bool reptileIgnoreShield;

    // Item Multipliers
    [HideInInspector] public float temporal_m;
    [HideInInspector] public float intimidation_m;
    [HideInInspector] public float blessing_m;
    [HideInInspector] public float vulnerability_m;
    [HideInInspector] public float lastStand_m;
    [HideInInspector] public float amplify_m;
    [HideInInspector] public float ballista_m;
    [HideInInspector] public float enchanted_m;
    [HideInInspector] public bool beastar_m;
    [HideInInspector] public float hardening_m;

    public void AddChampion(GameObject choosedChampion)
    {
        GameObject insChampion;
        insChampion = Instantiate(choosedChampion, transform);
        insChampion.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        champions.Add(insChampion);
        SetMutiplierValues();
    }

    public void SetMutiplierValues()
    {
        squirl_m = new float[] { 1, 1, 1 };
        beastDfs_m = 1;
        aquaticSlow_m = 1;
        birdCooldown_m = 1;
        plantHealth_m = 1;
        bugDmg_m = 1;
        reptileIgnoreShield = false;
        temporal_m = 1;
        intimidation_m = 1;
        blessing_m = 1;
        vulnerability_m = 1;
        lastStand_m = 1;
        amplify_m = 1;
        ballista_m = 1;
        enchanted_m = 1;
        beastar_m = false;
        hardening_m = 1;

        foreach (PlayerClass playerClass in playerClasses)
        {
            playerClass.value = 0;
            playerClass.isActive = false;
        }

        foreach (GameObject championGO in champions)
        {
            Champion champion = championGO.GetComponent<Champion>();
            champion.isEnchanted = false;

            // Class Multipliers
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
                                aquaticSlow_m = 0.85f;
                                break;
                            case Class.Plant:
                                plantHealth_m = 1.25f;
                                break;
                            case Class.Bug:
                                bugDmg_m = 1.25f;
                                break;
                            case Class.Reptile:
                                reptileIgnoreShield = true;
                                break;
                        }
                    }
                }
            }

            
            // Champion Multipliers
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
        // Item Multipliers
        foreach (var playerItem in playerItems)
        {
            switch (playerItem)
            {
                case "Temporal":
                    temporal_m = 0.9f;
                    break;
                case "Intimidation":
                    intimidation_m = 0.9f;
                    break;
                case "Blessing":
                    blessing_m = 1.2f;
                    break;
                case "Vulnerability":
                    vulnerability_m = 1.1f;
                    break;
                case "Amplify":
                    amplify_m = 1.2f;
                    break;
                case "Ballista":
                    ballista_m = 1.2f;
                    break;
                case "Enchanted":
                    enchanted_m = 0.67f;
                    Debug.Log("Yes");
                    Utility.RandomPick(champions).GetComponent<Champion>().isEnchanted = true;
                    break;
                case "Beastar":
                    beastar_m = true;
                    break;
                case "Hardening":
                    hardening_m = 1.5f;
                    break;
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
