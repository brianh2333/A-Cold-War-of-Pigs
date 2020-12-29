using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTroopSpawner : MonoBehaviour
{
    public GameObject[] spawnpoints;

    public int merits = 0;
    private int meritsSaved; //Saved value of merits, used for checking if the # of merits changes.
    public Text meritsText;

    //Modify costs in inspector
    public int troop1Cost = 10;
    public Text callTroop1Text;
    //public Text callTroop2Text;
    //public Text callTroop3Text;
    //public Text callTroop4Text;

    void Awake()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        meritsText.text = "Merits: " + merits;
        meritsSaved = merits;

        callTroop1Text.text = "Call Troop 1 \n" + "Cost: " + troop1Cost + " merits";
    }

    void Update()
    {
        if( meritsSaved != merits) //This only updates the amount of merits if the amount changes.
        {
            meritsSaved = merits;
            meritsText.text = "Merits: " + merits;
        }
    }
    public void SpawnTroop1()
    {
        if (merits >= troop1Cost)
        {
            merits -= troop1Cost;
            GameObject obj = PlayerPooler.instance.SpawnFromPool("Troop1", spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y + 120, obj.transform.eulerAngles.z);
            GameManager.instance.CIAAlive++;
        }
        else
            Debug.Log("Insufficient merits!");
    }
}
