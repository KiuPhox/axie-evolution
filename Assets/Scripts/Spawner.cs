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
    public GameObject[] spawners;
    public GameObject crossSpawn;
    public float spawnRadius;
    public GameObject enemy;
    public TMP_Text waveCount;

    [HideInInspector] public int currentWaveNumber;
    [HideInInspector] public int maxWaves;
    int enemiesReaminingAlive;

    [HideInInspector] public bool isStarted = false;

    ObjectPooler objectPooler;
    void Start()
    {
        seekerData.health = speedData.health = shooterData.health = 25f;
        seekerData.damage = speedData.damage = shooterData.damage = 8f;
        Utility.ShuffleArray(spawners);
        objectPooler = GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.GameStart && !isStarted)
        {
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
            if (currentWaveNumber - 1 < maxWaves)
            {
                Utility.ShuffleArray(spawners);

                string[] spawnTypes = { "4", "4+4", "4+4+4", "2x4", "3x4", "4x2"};
                string spawnType = spawnTypes[Utility.WeightPick(new float[] { 20, 20, 10, 15, 10, 15 })];

                enemiesReaminingAlive =  8 + Mathf.FloorToInt((float)GameManager.Instance.currentLevel / 2f);
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
                        StartCoroutine(SpawnEnemies(spawners[1], distributedEnemies[1], 3));
                        StartCoroutine(SpawnEnemies(spawners[2], distributedEnemies[2], 3));
                        break;
                    case "3x4":
                        distributedEnemies = Utility.DistributedSum(enemiesReaminingAlive, 4);
                        StartCoroutine(SpawnEnemies(spawners[0], distributedEnemies[0], 0));
                        StartCoroutine(SpawnEnemies(spawners[1], distributedEnemies[1], 3));
                        StartCoroutine(SpawnEnemies(spawners[2], distributedEnemies[2], 3));
                        StartCoroutine(SpawnEnemies(spawners[3], distributedEnemies[3], 3));
                        break;
                    case "4x2":
                        distributedEnemies = Utility.DistributedSum(enemiesReaminingAlive, 5);
                        StartCoroutine(SpawnEnemies(spawners[0], distributedEnemies[0], 0));
                        StartCoroutine(SpawnEnemies(spawners[1], distributedEnemies[1], 3));
                        StartCoroutine(SpawnEnemies(spawners[2], distributedEnemies[2], 3));
                        StartCoroutine(SpawnEnemies(spawners[3], distributedEnemies[3], 3));
                        StartCoroutine(SpawnEnemies(spawners[4], distributedEnemies[4], 3));
                        break;
                }
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.ChooseCard);
                seekerData.health = speedData.health = shooterData.health = 25 + 15 * (GameManager.Instance.currentLevel - 1);
                seekerData.damage = speedData.damage = shooterData.damage = 8 + 3 * (GameManager.Instance.currentLevel - 1);
                currentWaveNumber = 0;
            }
        }
    }


    private void OnEnemyDeath()
    {
        enemiesReaminingAlive--;
        if (enemiesReaminingAlive == 0)
        {
            isStarted = false;
        }
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
                i_enemy.GetComponent<Enemy>().SetCharacteristics();
                i_enemy.transform.SetParent(spawner.transform);
                i_enemy.transform.position = spawner.transform.position;
                i_enemy.transform.DOLocalMove(offsetSpawn, 0.2f);
                i_enemy.GetComponent<Enemy>().ResetAllEffect();
                i_enemy.GetComponent<Enemy>().OnDeath += OnEnemyDeath;

                
            }
        });
    }
}
