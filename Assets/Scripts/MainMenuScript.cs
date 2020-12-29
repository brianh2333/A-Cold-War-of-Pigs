using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    
    public GameObject middlePanel;
    public GameObject instructionsPanel;
    public GameObject creditsPanel;

    public void Start() {
        middlePanel.SetActive(true);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
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
}
