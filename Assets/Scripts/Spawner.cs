using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using TMPro;
using System.Linq;
public class Spawner : MonoBehaviour
{
    public ChampionData seekerData;
    public ChampionData speedData;
    public ChampionData shooterData;
    public ChampionData exploderData;
    public GameObject[] spawners;
    public GameObject crossSpawn;
    public float spawnRadius;
    public GameObject enemy;
    public TMP_Text waveCount;

    [HideInInspector] public int currentWaveNumber;
    [HideInInspector] public int maxWaves;
    int enemiesReaminingAlive;

    [HideInInspector] public bool isStarted = false;
    bool bossKilled = false;
    bool isSpawnBoss = false;
    void Start()
    {
        Utility.ShuffleArray(spawners);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossKilled)
        {
            if (enemiesReaminingAlive <= 0)
            {
                bossKilled = false;
                isSpawnBoss = false;
                GameManager.Instance.UpdateGameState(GameState.ChooseItem);
                currentWaveNumber = 0;
            }
        }
        else if (GameManager.Instance.State == GameState.GameStart && !isStarted)
        {
            bossKilled = false;
            isStarted = true;
            NextWave();
        }
    }

    float[] eliteSpawnWeight;
    string[] eliteSpawnType;
    float sumEliteSpawn;
    void NextWave()
    {
        if (GameManager.Instance.State == GameState.GameStart)
        {
            int currentLevel = GameManager.Instance.currentLevel;

            maxWaves = Level.levelToMaxWaves[currentLevel - 1];
            eliteSpawnWeight = Level.levelToEliteSpawnWeight[currentLevel - 1];
            eliteSpawnType = Level.levelToEliteSpawnType[currentLevel - 1];
            sumEliteSpawn = eliteSpawnWeight.Sum();

            currentWaveNumber++;
            waveCount.text = "Wave: " + currentWaveNumber + "/" + maxWaves;

            if (currentLevel % 5 == 0 && !isSpawnBoss)
            {

                isSpawnBoss = true;
                StartCoroutine(SpawnBoss(spawners[0], 0f));
            }

            if (currentWaveNumber - 1 < maxWaves)
            {
                Utility.ShuffleArray(spawners);

                string[] spawnTypes = { "4", "4+4", "4+4+4", "2x4", "3x4", "4x2"};
                string spawnType = spawnTypes[Utility.WeightPick(new float[] { 20, 20, 10, 15, 10, 15 })];

                enemiesReaminingAlive =  8 + Mathf.FloorToInt((float)currentLevel / 2f);
                int[] distributedEnemies;

                switch (spawnType)
                { 
                    case "4":
                        StartCoroutine(SpawnEnemies(spawners[0], enemiesReaminingAlive, 0));
                        break;
                    case "4+4":
                        distributedEnemies = Utility.DistributedSum(enemiesReaminingAlive, 2);
                        StartCoroutine(SpawnEnemies(spawners[0], distributedEnemies[0], 0));
                        StartCoroutine(SpawnEnemies(spawners[1], distributedEnemies[1], 0));
                        break;
                    case "4+4+4":
                        distributedEnemies = Utility.DistributedSum(enemiesReaminingAlive, 3);
                        StartCoroutine(SpawnEnemies(spawners[0], distributedEnemies[0], 0));
                        StartCoroutine(SpawnEnemies(spawners[1], distributedEnemies[1], 0));
                        StartCoroutine(SpawnEnemies(spawners[2], distributedEnemies[2], 0));
                        break;
                    case "2x4":
                        distributedEnemies = Utility.DistributedSum(enemiesReaminingAlive, 3);
                        StartCoroutine(SpawnEnemies(spawners[0], distributedEnemies[0], 0));
                        StartCoroutine(SpawnEnemies(spawners[1], distributedEnemies[1], 2.5f));
                        StartCoroutine(SpawnEnemies(spawners[2], distributedEnemies[2], 2.5f));
                        break;
                    case "3x4":
                        distributedEnemies = Utility.DistributedSum(enemiesReaminingAlive, 4);
                        StartCoroutine(SpawnEnemies(spawners[0], distributedEnemies[0], 0));
                        StartCoroutine(SpawnEnemies(spawners[1], distributedEnemies[1], 2.5f));
                        StartCoroutine(SpawnEnemies(spawners[2], distributedEnemies[2], 2.5f));
                        StartCoroutine(SpawnEnemies(spawners[3], distributedEnemies[3], 2.5f));
                        break;
                    case "4x2":
                        distributedEnemies = Utility.DistributedSum(enemiesReaminingAlive, 5);
                        StartCoroutine(SpawnEnemies(spawners[0], distributedEnemies[0], 0));
                        StartCoroutine(SpawnEnemies(spawners[1], distributedEnemies[1], 2.5f));
                        StartCoroutine(SpawnEnemies(spawners[2], distributedEnemies[2], 2.5f));
                        StartCoroutine(SpawnEnemies(spawners[3], distributedEnemies[3], 2.5f));
                        StartCoroutine(SpawnEnemies(spawners[4], distributedEnemies[4], 2.5f));
                        break;
                }
            }
            else if (currentLevel >= 25)
            {
                GameManager.Instance.UpdateGameState(GameState.GameVictory);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.ChooseCard);
                currentWaveNumber = 0;
            }
        }
    }


    private void OnEnemyDeath()
    {
        enemiesReaminingAlive--;
        if (enemiesReaminingAlive <= 0)
        {
            isStarted = false;
        }
    }

    private void OnBossDeath()
    {
        bossKilled = true;
        Debug.Log("Yes");
    }

    IEnumerator SpawnBoss(GameObject spawner, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        // Instantiate Spawn Cross
        GameObject i_cross = Instantiate(crossSpawn, spawner.transform);
        Color spawnCrossColor = i_cross.GetComponent<SpriteRenderer>().color;
        spawnCrossColor.a = 0f;
        i_cross.GetComponent<SpriteRenderer>().color = spawnCrossColor;

        i_cross.GetComponent<SpriteRenderer>().DOFade(1f, 0.2f).SetLoops(8, LoopType.Yoyo).SetDelay(0.5f).OnComplete(() =>
        {
            Destroy(i_cross);

            string bossTag = "";

            switch (GameManager.Instance.currentLevel){
                case 5:
                    bossTag = "b_seeker";
                    break;
                case 10:
                    bossTag = "b_speed";
                    break;
                case 15:
                    bossTag = "b_shooter";
                    break;
                case 20:
                    bossTag = "b_seeker";
                    break;
                case 25:
                    bossTag = "b_header";
                    break;
            }
            
            GameObject i_boss = ObjectPooler.Instance.GetPooledObject(bossTag);

            i_boss.SetActive(true);
            i_boss.GetComponent<LivingEntity>().SetCharacteristicsForBoss();
            i_boss.transform.SetParent(spawner.transform);
            i_boss.transform.position = spawner.transform.position + new Vector3(0, 0, Random.Range(-0.1f, 0.1f));
            i_boss.GetComponent<Enemy>().ResetAllEffect();
            i_boss.GetComponent<Enemy>().OnDeath += OnBossDeath;
        });
    }
    IEnumerator SpawnEnemies(GameObject spawner, int enemiesToSpawn, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        // Instantiate Spawn Cross
        GameObject i_cross = Instantiate(crossSpawn, spawner.transform);
        Color spawnCrossColor = i_cross.GetComponent<SpriteRenderer>().color;
        spawnCrossColor.a = 0f;
        i_cross.GetComponent<SpriteRenderer>().color = spawnCrossColor;
        
        // Spawn Enemy
        i_cross.GetComponent<SpriteRenderer>().DOFade(1f, 0.2f).SetLoops(8, LoopType.Yoyo).SetDelay(0.5f).OnComplete(() =>
        {
            Destroy(i_cross);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                GameObject i_enemy;

                // Spawn Elite
                if (Utility.RandomBool(sumEliteSpawn))
                {
                    string eliteType = eliteSpawnType[Utility.WeightPick(eliteSpawnWeight)];
                    i_enemy = ObjectPooler.Instance.GetPooledObject(eliteType);
                }
                else
                {
                    i_enemy = ObjectPooler.Instance.GetPooledObject("seeker");
                }

                float x = Random.Range(-spawnRadius, spawnRadius);
                float y = Random.Range(-spawnRadius, spawnRadius);
                Vector3 offsetSpawn = new Vector3(x, y, 0);

                i_enemy.SetActive(true);
                i_enemy.GetComponent<LivingEntity>().SetCharacteristicsForEnemy();
                i_enemy.transform.SetParent(spawner.transform);
                i_enemy.transform.position = spawner.transform.position;
                i_enemy.transform.DOLocalMove(offsetSpawn, 0.2f);
                i_enemy.GetComponent<Enemy>().ResetAllEffect();
                i_enemy.GetComponent<Enemy>().OnDeath += OnEnemyDeath;
            }
        });
    }
}
