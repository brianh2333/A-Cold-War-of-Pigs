using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTroopSpawner : MonoBehaviour
{
    public GameObject[] spawnpoints;

    void Start()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");
    }
    public void SpawnTroop1()
    {
        GameObject obj = PlayerPooler.instance.SpawnFromPool("Troop1", spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position, Quaternion.identity);
        obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y + 120, obj.transform.eulerAngles.z);
    }
}
