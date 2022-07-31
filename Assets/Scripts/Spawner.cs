using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject crossSpawn;
    public float spawnRadius;
    public GameObject enemy;
    public Wave[] waves;

    

    Wave currentWave;
    [HideInInspector] public int currentWaveNumber;
    int enemiesPerSpawner;
    int enemiesReaminingAlive;

    bool isStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Utility.ShuffleArray(spawners);
        
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
            currentWaveNumber++;

            if (currentWaveNumber - 1 < waves.Length)
            {
                currentWave = waves[currentWaveNumber - 1];
                enemiesReaminingAlive = currentWave.enemyCount;
                enemiesPerSpawner = currentWave.enemyCount / currentWaveNumber;
            }

            Utility.ShuffleArray(spawners);

            for (int i = 0; i < currentWaveNumber && i < spawners.Length - 1; i++)
            {
                SpawnEnemies(spawners[i]);
            }
        }
    }


    private void OnEnemyDeath()
    {
        enemiesReaminingAlive--;
        if (enemiesReaminingAlive == 0)
        {
            GameManager.Instance.UpdateGameState(GameState.ChooseCard);
            isStarted = false;
        }
    }

    private void SpawnEnemies(GameObject spawner)
    {
        // Instantiate Spawn Cross
        GameObject i_cross = Instantiate(crossSpawn, spawner.transform);
        Color spawnCrossColor = i_cross.GetComponent<SpriteRenderer>().color;
        spawnCrossColor.a = 0f;
        i_cross.GetComponent<SpriteRenderer>().color = spawnCrossColor;

        // Spawn Enemy
        i_cross.GetComponent<SpriteRenderer>().DOFade(1f, 0.2f).SetLoops(8, LoopType.Yoyo).SetDelay(0.5f).OnComplete(() =>
        {
            Destroy(i_cross);
            for (int i = 0; i < enemiesPerSpawner; i++)
            {
                float x = Random.Range(-spawnRadius, spawnRadius);
                float y = Random.Range(-spawnRadius, spawnRadius);
                Vector3 offsetSpawn = new Vector3(x, y, 0);

                GameObject i_enemy = Instantiate(enemy, spawner.transform);

                i_enemy.transform.DOLocalMove(offsetSpawn, 0.2f);
                i_enemy.GetComponent<Enemy>().OnDeath += OnEnemyDeath;
            }
        });  
    }


    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
    }
}
