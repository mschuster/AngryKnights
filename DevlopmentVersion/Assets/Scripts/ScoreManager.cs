/*
 * ScoreManager class
 *
 * Keeps track of the duration of the game time. Saves infos about needed shots and killed enemys.
 * Calculates score from saved information.
 * Writes the score into the responsible score csv file.
 * 
 * Author: Martin Schuster
 */

using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int playTime;
    private int kills;
    private int shots;
    public GameObject scoreSavePanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreSaveNamePlaceHolder;
    public TMP_InputField scoreSaveName;
    public TextMeshProUGUI scoreTextFinal;
    public TextMeshProUGUI scoreTime;
    public TextMeshProUGUI gameScore;
    public TextMeshProUGUI shotsScore;
    public TextMeshProUGUI lifePenalty;
    public Tower tower;
    public SceneChanger sceneChanger;

    private void Start()
    {
        sceneChanger = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneChanger>();
        UpdateScoreText();
        StartCoroutine(Timer());
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            playTime++;
        }
    }

    public void IncreaseKills()
    {
        kills++;
    }

    public void IncreaseShots()
    {
        shots++;
    }

    public void CalcFinalScore()
    {
        var tmpScore = 0;
        gameScore.text = score.ToString();
        score -= (int) ((playTime * 5) + (shots * 10));
        tmpScore = score;
        score = (int)(score * (tower.GetLifePoints()/3f));
        scoreTime.text = (-1*playTime * 5).ToString();
        shotsScore.text = (-1*shots * 10).ToString();
        lifePenalty.text = (tmpScore - score).ToString();
        scoreTextFinal.text = score.ToString();
    }

    public void SaveHighScorePanel()
    {
        var tmpName = Environment.UserName;
        scoreSavePanel.SetActive(true);
        scoreSaveNamePlaceHolder.text = tmpName;
    }

    public void SaveHighScore()
    {
        var path= "";
        switch (InterchangableData.DifficultySetting)
        {
            case Difficulty.Easy:
                path = Application.dataPath + "/StreamingAssets/Score_Easy.csv";
                break;
            case Difficulty.Medium:
                path = Application.dataPath + "/StreamingAssets/Score_Medium.csv";
                break;
            case Difficulty.Hard:
                path = Application.dataPath + "/StreamingAssets/Score_Hard.csv";
                break;
        }

        using (var writer = new StreamWriter(path,true))
        {
            if (string.IsNullOrEmpty(scoreSaveName.text))
            {
                writer.WriteLine($"{scoreSaveNamePlaceHolder.text};{score.ToString()}");
                writer.Close();
            }
            else
            {
                writer.WriteLine($"{scoreSaveName.text};{score.ToString()}");
                writer.Close();
            }
        }
        sceneChanger.ChangeScene(0);
    }
}