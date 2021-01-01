using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    
    public GameObject middlePanel;
    public GameObject instructionsPanel;
    public GameObject creditsPanel;

    public GameObject historyText;
    public GameObject background;

    public Animator backgroundAnim;
    public Animator historyAnim;

    public void Start() {
        historyText.SetActive(false);
        middlePanel.SetActive(true);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void StartGame() {
        StartCoroutine(StartPressed());
    }

    public void Instructions() {
        middlePanel.SetActive(false);
        instructionsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void Credits() {
        middlePanel.SetActive(false);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void Back() {
        middlePanel.SetActive(true);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Skip()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator StartPressed()
    {
        backgroundAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        background.SetActive(false);
        historyText.SetActive(true);
        historyAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene(1);
    }
}
