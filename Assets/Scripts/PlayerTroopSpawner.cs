using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTroopSpawner : MonoBehaviour
{
    public static PlayerTroopSpawner instance; //Singleton
    public GameObject[] spawnpoints;

    public int merits = 0;
    private int meritsSaved; //Saved value of merits, used for checking if the # of merits changes.
    public Text meritsText;

    //Modify costs in inspector
    public int riflemanCost = 10;
    public int gunnerCost = 30;
    public int sniperCost = 75;
    public Text callRiflemanText;
    public Text callGunnerText;
    public Text callSniperText;
    //public Text callTroop2Text;
    //public Text callTroop3Text;
    //public Text callTroop4Text;

    void Awake()
    {
        if (instance == null) instance = this;
        spawnpoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        meritsText.text = "Merits: " + merits;
        meritsSaved = merits;

        callRiflemanText.text = "Call Rifleman \n" + "Cost: " + riflemanCost + " merits";
        callGunnerText.text = "Call Gunner \n" + "Cost: " + gunnerCost + " merits";
        callSniperText.text = "Call Sniper \n" + "Cost: " + sniperCost + " merits";
    }

    void Update()
    {
        if( meritsSaved != merits) //This only updates the amount of merits if the amount changes.
        {
            meritsSaved = merits;
            meritsText.text = "Merits: " + merits;
        }
    }
    public void SpawnRifleman()
    {
        if (merits >= riflemanCost)
        {
            merits -= riflemanCost;
            GameObject obj = PlayerPooler.instance.SpawnFromPool("Rifleman", spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y + 120, obj.transform.eulerAngles.z);
            GameManager.instance.CIAAlive++;
        }
        else
            Debug.Log("Insufficient merits!");
    }

    public void SpawnGunner()
    {
        if (merits >= gunnerCost)
        {
            merits -= gunnerCost;
            GameObject obj = PlayerPooler.instance.SpawnFromPool("Gunner", spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y + 120, obj.transform.eulerAngles.z);
            GameManager.instance.CIAAlive++;
        }
        else
            Debug.Log("Insufficient merits!");
    }

    public void SpawnSniper()
    {
        if (merits >= sniperCost)
        {
            merits -= sniperCost;
            GameObject obj = PlayerPooler.instance.SpawnFromPool("Sniper", spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y + 120, obj.transform.eulerAngles.z);
            GameManager.instance.CIAAlive++;
        }
        else
            Debug.Log("Insufficient merits!");
    }

    //will be modified when enemies are added.
    public void AddMerits(int amount)
    {
        merits += amount;
    }
}
