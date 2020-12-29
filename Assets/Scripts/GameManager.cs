using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance; //Creating GameManager singleton

    public Animator troopsUIAnimator;

    public Text CIAAliveText;
    public Text targetsRemainingText;

    public int CIAAlive = 0;
    public int targetsRemaining = 0;

    bool troopsUIOpen = false;

    void Awake() {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;
        DisplayCounters();
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
