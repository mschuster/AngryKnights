/*
 * MainMenu class
 *
 * Simple class for the button handling in the Menu scene. 
 *
 * Author: Martin Schuster 
 */

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject newGamePanel;
    public GameObject highScorePanel;
    public GameObject instructionsPanel;
    
    public void StartNewGame()
    {
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(true);
    }

    public void Highscores()
    {
        mainMenuPanel.SetActive(false);
        highScorePanel.SetActive(true);
    }

    public void Instructions()
    {
        instructionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void MainMenuPanel()
    {
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    public void ExitApp()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
