using System.Collections;
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
    public GameObject playerTroopSpawns;
    public GameObject enemyTroopSpawns;
    public GameObject pauseMenu;
    public Camera camera;


    void Awake() {
        retryPanel.SetActive(false);
        pauseMenu.SetActive(false);
        hour++;
        CIAAlive = 0;
        targetsRemaining = 0;
        if (instance == null) instance = this;

        camera.transform.position = new Vector3(-2.8f, 1.9f, -6.3f);
        playerTroopSpawns.transform.position = new Vector3(-5.27f, 0.2763095f, .03f);
        enemyTroopSpawns.transform.position = new Vector3(3.93f, 0.2763095f, .03f);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;
        DisplayCounters();
        DisplayHour();

        RetryPanel();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            ResumeGame();
        }
    }

    public void MoveCamera(bool move)
    {
        if (move == true)
        {
            camera.transform.Translate(Vector2.right * Time.deltaTime * 30f);
            playerTroopSpawns.transform.Translate(Vector2.right * Time.deltaTime * 30f);
            enemyTroopSpawns.transform.Translate(Vector2.right * Time.deltaTime * 30f); ;
        }
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

    public void ResumeGame() {
        if (Time.timeScale == 1f) {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void QuitGame() {
        SceneManager.LoadScene(0);
    }
}
