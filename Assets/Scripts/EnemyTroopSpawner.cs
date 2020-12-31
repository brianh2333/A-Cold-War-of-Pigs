using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTroopSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        START,
        SPAWNING,
        WAITING,
        FINISHED
    }
    public SpawnState state = SpawnState.START;

    [System.Serializable]
    public class Wave
    {
        [System.Serializable]
        public class Enemy
        {
            public string name;
            public int amount;
            public GameObject enemy;
        }
        public string name;
        public Enemy[] enemies;
        public int amount;
    }

    public Wave[] waves;

    private int hourIndex = 0;
    private int enemyIndex = 0;

    public GameObject[] spawnpoints;
    public GameObject[] emplacements;

    public int totalAmount;
    private bool spawn = true;

    int empIndex = 0;
    int spawnIndex = 0;

    void Awake()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("EnemySpawns");
        emplacements = GameObject.FindGameObjectsWithTag("EnemyEmplacements");
        hourIndex = 0;
        totalAmount = 0;
        empIndex = 0;
        spawnIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if ((hourIndex != GameManager.instance.hour-1 || (state == SpawnState.START || state == SpawnState.SPAWNING)) || spawn)
        {

            if (spawn)
            {
                for (int i = 0; i < waves[hourIndex].enemies.Length; i++)
                {
                    totalAmount += waves[hourIndex].enemies[i].amount;
                }
                spawn = false;
            }
            for (int i = 0; i < waves[hourIndex].amount; i++)
            {
                state = SpawnState.SPAWNING;
                StartCoroutine(Spawn());
                spawnIndex++;
            }
            spawn = false;
            if (spawnIndex == totalAmount && state == SpawnState.SPAWNING) 
            {
                state = SpawnState.WAITING;
            }
            else if (state == SpawnState.WAITING && GameManager.instance.targetsRemaining == 0)
            {
                state = SpawnState.FINISHED;
            }
        }
        else if (state == SpawnState.FINISHED)
        {
            totalAmount = 0;
            enemyIndex = 0;
            spawnIndex = 0;
            hourIndex++;
            GameManager.instance.hour++;
            spawn = true;
        }
    }

    IEnumerator Spawn()
    {
        if ( spawnIndex == waves[hourIndex].enemies[enemyIndex].amount)
        {
            enemyIndex++;
        }
        if (waves[hourIndex].enemies[enemyIndex].enemy.name.Contains("Gunner"))
        {
            GameObject obj = EnemyPooler.instance.SpawnFromPool(waves[hourIndex].enemies[enemyIndex].name, emplacements[empIndex].transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y - 90, obj.transform.eulerAngles.z);
            empIndex = (empIndex + 1) % 3;
            yield return null;
        }
        else
        {
            GameObject obj = EnemyPooler.instance.SpawnFromPool(waves[hourIndex].enemies[enemyIndex].name, spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y - 90, obj.transform.eulerAngles.z);
            yield return new WaitForSeconds(3f);
        }
        GameManager.instance.targetsRemaining++;
    }
}
