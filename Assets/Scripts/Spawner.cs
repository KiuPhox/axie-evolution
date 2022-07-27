using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject enemy;
    public Wave[] waves;

    Wave currentWave;
    int currentWaveNumber;
    int enemiesReaminingAlive;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnemyDeath()
    {
        enemiesReaminingAlive--;
        if (enemiesReaminingAlive == 0)
        {
            //NextWave();
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
    }
}
