using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawners;
    public float spawnRadius;
    public GameObject enemy;
    public Wave[] waves;

    Wave currentWave;
    int currentWaveNumber;
    int enemiesPerSpawner;
    int enemiesReaminingAlive;


    // Start is called before the first frame update
    void Start()
    {
        Utility.ShuffleArray(spawners);
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < currentWaveNumber && i < spawners.Length - 1; i++)
            {
                SpawnEnemies(spawners[i]);
            }
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];
            enemiesReaminingAlive = currentWave.enemyCount;
            enemiesPerSpawner = currentWave.enemyCount / currentWaveNumber;
            Debug.Log(enemiesPerSpawner);
        }
        Utility.ShuffleArray(spawners);
        /*
        if (OnNewWave != null)
        {
            OnNewWave(currentWaveNumber);
        }
        ResetPlayerPosition();
        */
    }

    void OnEnemyDeath()
    {
        enemiesReaminingAlive--;
        if (enemiesReaminingAlive == 0)
        {
            //NextWave();
        }
    }

    private void SpawnEnemies(GameObject spawner)
    {
        for (int i = 0; i < enemiesPerSpawner; i++) {

            float x = Random.Range(-spawnRadius, spawnRadius);
            float y = Random.Range(-spawnRadius, spawnRadius);
            Vector3 offsetSpawn = new Vector3(x, y, 0);

            GameObject i_enemy = Instantiate(enemy, spawner.transform);

            i_enemy.transform.DOLocalMove(offsetSpawn, 0.2f);
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
    }
}
