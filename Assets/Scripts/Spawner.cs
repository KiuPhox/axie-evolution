using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using TMPro;
public class Spawner : MonoBehaviour
{
    public ChampionData enemyData;
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
        enemyData.health = 25f;
        enemyData.damage = 8f;
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
    void NextWave()
    {
        if (GameManager.Instance.State == GameState.GameStart)
        {
            maxWaves = Level.levelToMaxWaves[GameManager.Instance.currentLevel - 1];
            currentWaveNumber++;
            waveCount.text = "Wave: " + currentWaveNumber + "/" + maxWaves;
            if (currentWaveNumber - 1 < maxWaves)
            {
                Utility.ShuffleArray(spawners);

                string[] spawnTypes = { "4", "4+4", "4+4+4", "2x4", "3x4", "4x2"};
                string spawnType = spawnTypes[Utility.WeightPick(new float[] { 20, 20, 10, 15, 10, 15 })];
                Debug.Log(spawnType);

                switch (spawnType)
                { 
                    case "4":
                        StartCoroutine(SpawnEnemies(spawners[0], 9, 0));
                        enemiesReaminingAlive = 9;
                        break;
                    case "4+4":
                        StartCoroutine(SpawnEnemies(spawners[0], 4, 0));
                        StartCoroutine(SpawnEnemies(spawners[1], 5, 0));
                        enemiesReaminingAlive = 9;
                        break;
                    case "4+4+4":
                        StartCoroutine(SpawnEnemies(spawners[0], 2, 0));
                        StartCoroutine(SpawnEnemies(spawners[1], 3, 0));
                        StartCoroutine(SpawnEnemies(spawners[2], 4, 0));
                        enemiesReaminingAlive = 9;
                        break;
                    case "2x4":
                        StartCoroutine(SpawnEnemies(spawners[0], 5, 0));
                        StartCoroutine(SpawnEnemies(spawners[1], 2, 3));
                        StartCoroutine(SpawnEnemies(spawners[2], 2, 3));
                        enemiesReaminingAlive = 9;
                        break;
                    case "3x4":
                        StartCoroutine(SpawnEnemies(spawners[0], 5, 0));
                        StartCoroutine(SpawnEnemies(spawners[1], 1, 3));
                        StartCoroutine(SpawnEnemies(spawners[2], 1, 3));
                        StartCoroutine(SpawnEnemies(spawners[3], 2, 3));
                        enemiesReaminingAlive = 9;
                        break;
                    case "4x2":
                        StartCoroutine(SpawnEnemies(spawners[0], 5, 0));
                        StartCoroutine(SpawnEnemies(spawners[1], 1, 3));
                        StartCoroutine(SpawnEnemies(spawners[2], 1, 3));
                        StartCoroutine(SpawnEnemies(spawners[3], 1, 3));
                        StartCoroutine(SpawnEnemies(spawners[4], 1, 3));
                        enemiesReaminingAlive = 9;
                        break;
                }
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.ChooseCard);
                enemyData.health  = 25 + 15 * (GameManager.Instance.currentLevel - 1);
                enemyData.damage = 8 + 3 * (GameManager.Instance.currentLevel - 1);
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
                float x = Random.Range(-spawnRadius, spawnRadius);
                float y = Random.Range(-spawnRadius, spawnRadius);
                Vector3 offsetSpawn = new Vector3(x, y, 0);

                GameObject i_enemy = ObjectPooler.Instance.GetPooledObject();

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
