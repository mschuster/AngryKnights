/*
 * SceneChanger class
 *
 * Loads new scene asynchronous.
 * Used to return to Menu from gameplay scene.
 *
 * Author: Martin Schuster
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int sceneID)
    {
        Time.timeScale = 1;
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
}
