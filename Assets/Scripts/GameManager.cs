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
    public Text waveText;


    public int CIAAlive = 0;
    public int targetsRemaining = 0;
    public int hour;
    public int wave;

    bool troopsUIOpen = false;

    public GameObject retryPanel;
    public GameObject playerTroopSpawns;
    public GameObject enemyTroopSpawns;
    public GameObject pauseMenu;
    public GameObject historyText;
    public GameObject[] waypoints;
    public Transform mover;
    public Camera camera;
    public GameObject dayNightLight;

    private int waypointIndex;


    void Awake() {
        waypointIndex = 0;
        hour = 1;
        retryPanel.SetActive(false);
        pauseMenu.SetActive(false);
        wave++;
        CIAAlive = 0;
        targetsRemaining = 0;
        if (instance == null) instance = this;


        mover.transform.position = waypoints[0].transform.position;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (hour == 20)
            StartCoroutine(FinalCutscene());
        targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;
        DisplayCounters();
        DisplayHour();

        RetryPanel();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            ResumeGame();
        }

        if (hour == 2 % 3) {
            dayNightLight.transform.eulerAngles = new Vector3(50f, -30f, 0);
        }
        else {
            dayNightLight.transform.eulerAngles = new Vector3(15f, -160f,  -150f);
        }
    }


    public IEnumerator MoveCamera(bool move, float duration)
    {
        if (move == true && waypoints[waypointIndex] != null)
        {
            float time = 0;
            waypointIndex++;
            while (time < duration)
            {
                mover.transform.position = Vector3.Lerp(waypoints[waypointIndex - 1].transform.position, waypoints[waypointIndex].transform.position, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            mover.transform.position = waypoints[waypointIndex].transform.position;
        }
        else
            yield return null;
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
        waveText.text = "Wave " + wave + " of the invasion.";
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

    public IEnumerator FinalCutscene()
    {
        historyText.SetActive(true);
        historyText.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene(0);
    }
}
