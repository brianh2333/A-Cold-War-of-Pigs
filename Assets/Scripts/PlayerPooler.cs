using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPooler : MonoBehaviour
{
    //This script is specifically for the player's troops.

    public static PlayerPooler instance; //Singleton


    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size; //Amount to spawn in.
    }

    //This will save us from hardcoding pools for each troop type.
    public List<Pool> pools; //A pool for each player troop
    public Dictionary<string, Queue<GameObject>> poolDict; //A dict so we can spawn a troop based on player troop name

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);


        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);

                if (obj.CompareTag("Bullet"))
                    obj.transform.parent = GameObject.Find("Bullets").transform;

                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDict.Add(pool.name, objectPool);

        }
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (poolDict.ContainsKey(name))
        {
            GameObject spawnedObj = poolDict[name].Dequeue();

            spawnedObj.SetActive(true);
            spawnedObj.transform.position = position;
            spawnedObj.transform.rotation = rotation;


            poolDict[name].Enqueue(spawnedObj);

            return spawnedObj;
        }
        else
        {
            Debug.LogWarning("Pool w/ name " + name + " does not exist.");
            return null;
        }
    }

    public GameObject SpawnFromPool(string name)
    {
        if (poolDict.ContainsKey(name))
        {
            GameObject spawnedObj = poolDict[name].Dequeue();

            spawnedObj.SetActive(true);

            poolDict[name].Enqueue(spawnedObj);

            return spawnedObj;
        }
        else
        {
            Debug.LogWarning("Pool w/ name " + name + " does not exist.");
            return null;
        }
    }
}
