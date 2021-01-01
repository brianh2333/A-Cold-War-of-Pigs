﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance; //Creating GameManager singleton

    public Animator troopsUIAnimator;

    public Text CIAAliveText;
    public Text targetsRemainingText;
    public Text hourText;

    public int CIAAlive = 0;
    public int targetsRemaining = 0;
    public int hour;

    bool troopsUIOpen = false;

    public GameObject retryPanel;


    void Awake() {
        retryPanel.SetActive(false);
        hour++;
        CIAAlive = 0;
        targetsRemaining = 0;
        if (instance == null) instance = this;


    }

    private void Update()
    {
        targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;
        DisplayCounters();
        DisplayHour();

        RetryPanel();
    }

    public void RetryPanel()
    {
        if (PlayerTroopSpawner.instance.merits < PlayerTroopSpawner.instance.riflemanCost && CIAAlive == 0 && targetsRemaining > 0 )
        {
            retryPanel.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void DisplayHour()
    {
        hourText.text = "Hour " + hour;
    }

    public void DisplayCounters()
    {
        CIAAliveText.text = "CIA alive: " + CIAAlive;
        targetsRemainingText.text = "Targets remaining: " + targetsRemaining;
    }

    public void TroopsUIHover() {
        if (troopsUIOpen == false) {
            troopsUIAnimator.SetBool("Hovered", true);
            troopsUIOpen = true;
            TroopsUIHovered();
        }
    }
    
    public void TroopsUIExit() {
        if (troopsUIOpen == true) {
            troopsUIOpen = false;
            troopsUIAnimator.SetBool("Hovered", false);
        }
    }


    public void TroopsUIHovered() {
        troopsUIOpen = true;
    }

    public void TroopsUIClosed() {
        troopsUIOpen = true;
        TroopsUIExit();
    }
}
