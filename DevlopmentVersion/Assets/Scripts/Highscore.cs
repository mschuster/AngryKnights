/*
 * Highscore class
 *
 * Responsible for reading the csv score files depending on chosen difficulty.
 * Displays score list descending by score value.
 *
 * Author: Martin Schuster
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI scoreList;
    private string path;
    private StreamReader reader;
    private List<Tuple<string, int>> scoreListing;
    public GameObject mainMenuPanel;
    public GameObject highScorePanel;
    
    private void Start()
    {
        path = Application.dataPath + "/StreamingAssets/Score_Easy.csv";
        ReadCSV();
    }

    private void ReadCSV()
    {
        scoreList.text = "";
        scoreListing = new List<Tuple<string, int>>();
        using (reader = new StreamReader(path))
        {
            var line = "";
            string[] values;
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                values = line.Split(';');
                scoreListing.Add(Tuple.Create(values[0], int.Parse(values[1])));
            }
        }

        var ordertList = scoreListing.OrderByDescending(x => x.Item2);
        var i = 1;
        foreach (var item in ordertList)
        {
            scoreList.text += $"{i} {item.Item1} ... {item.Item2} \n";
            i++;
        }

        reader.Close();
    }

    public void ChangeScoreList(string difficult)
    {
        switch (difficult)
        {
            case "Einfach":
                path = Application.dataPath + "/StreamingAssets/Score_Easy.csv";
                break;
            case "Mittel":
                path = Application.dataPath + "/StreamingAssets/Score_Medium.csv";
                break;
            case "Schwer":
                path = Application.dataPath + "/StreamingAssets/Score_Hard.csv";
                break;
        }
        ReadCSV();
    }

    public void OpenMainMenu()
    {
        highScorePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
