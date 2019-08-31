/*
 * NewGameSetup class
 *
 * Writes the chosen difficulty setting to the InterchangableData class.
 * Responsible for loading the game scene.
 * 
 * Author: Martin Schuster
 */

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewGameSetup : MonoBehaviour
{
    private string difficult;
    public TMP_Dropdown difficultSelection;
    public GameObject mainMenuPanel;
    public GameObject newGamePanel;

    private void Start()
    {
        ChangeDifficulty();
    }

    public void ChangeDifficulty()
    {
        difficult = difficultSelection.options[difficultSelection.value].text;
    }
    
    public void ChangeScene(int sceneID)
    {
        InterchangableData.SetDifficulty(difficult);
        StartCoroutine(LoadAsynchronously(sceneID));
    }
    
    IEnumerator LoadAsynchronously(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
    
    public void OpenMainMenu()
    {
        newGamePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
