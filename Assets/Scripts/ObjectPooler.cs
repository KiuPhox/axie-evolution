using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        [TableColumnWidth(50, Resizable = false)]
        public int size;
    }

    [TableList]
    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;
  
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach(Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            if (!poolDictionary[tag][i].activeInHierarchy)
            {
                return poolDictionary[tag][i];
            }
        }
        return null;
    }

    public List<GameObject> GetEnemyPool()
    {
        return poolDictionary["seeker"].Concat(poolDictionary["shooter"]).ToList().Concat(poolDictionary["exploder"])
            .ToList().Concat(poolDictionary["speed"]).ToList().Concat(poolDictionary["head"])
            .ToList().Concat(poolDictionary["b_seeker"]).ToList();
    }

    public GameObject GetPooledObjectIndex(string tag, int index)
    {
        return poolDictionary[tag][index];
    }
}
