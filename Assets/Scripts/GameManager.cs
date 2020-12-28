using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance; //Creating GameManager singleton

    public Animator troopsUIAnimator;

    bool troopsUIOpen = false;

    void Awake() {
        if (instance == null) instance = this;
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
