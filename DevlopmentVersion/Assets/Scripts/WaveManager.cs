/*
 * WaveManager class
 *
 * Sets values depending on chosen difficulty for the wave attacks.
 * Handles pause menu. Keeps track of remaining unity and victory condition.
 * 
 * Author: Martin Schuster 
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    public GameObject agent;
    public TextMeshProUGUI announcer;
    private int baseAmount;
    private float countdownTimer = 3f;
    private int remainingUnits = 0;
    private int currentWave = 1;
    public Difficulty difficulty;
    private float agentSpeed;
    private int waveScore;
    public GameObject mobs;
    public RandomSpawnPoint randomSpawnPoint;
    private float spawnTimer;
    private int waves;
    public List<GameObject> enemyPool;
    public GameObject gameStatePanel;
    private float waveTimer;
    public GameObject victoryPanel;

    // Start is called before the first frame update
    private void Start()
    {
        difficulty = InterchangableData.DifficultySetting;
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        mobs = new GameObject {name = "mobs"};
        switch (difficulty)
        {
            case Difficulty.Easy:
                waves = 3;
                baseAmount = 3;
                spawnTimer = 5f;
                agentSpeed = 1f;
                waveScore = 1000;
                break;
            case Difficulty.Medium:
                waves = 5;
                baseAmount = 5;
                spawnTimer = 3f;
                agentSpeed = 2f;
                waveScore = 2000;
                break;
            case Difficulty.Hard:
                waves = 7;
                baseAmount = 7;
                spawnTimer = 1.5f;
                agentSpeed = 3f;
                waveScore = 3000;
                break;
        }
        remainingUnits = waves * baseAmount;
        waveTimer = spawnTimer * baseAmount + 10f;
        announcer.text = "";
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (countdownTimer > 0)
        {
            announcer.text = $"Spiel begint in ... {countdownTimer}";
            yield return new WaitForSeconds(1);
            countdownTimer--;
        }
        announcer.text = "Los!";
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        while (currentWave <= waves)
        {
            announcer.enabled = true;
            announcer.text = $"Welle {currentWave} beginnt!";
            scoreManager.IncreaseScore(waveScore);
            yield return new WaitForSeconds(2f);
            announcer.enabled = false;
            for (var i = 0; i < baseAmount; i++)
            {
                GameObject knight = Instantiate(enemyPool[Random.Range(0,enemyPool.Count-1)], randomSpawnPoint.SpawnPoint(), Quaternion.identity,
                    mobs.transform);
                knight.GetComponent<NavMeshAgent>().speed = agentSpeed;
                yield return new WaitForSeconds(spawnTimer);
            }
            yield return new WaitForSeconds(waveTimer);
            currentWave++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            gameStatePanel.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        gameStatePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void DecreaseRemainingUnits()
    {
        remainingUnits--;
        if (remainingUnits == 0)
        {
            GameVictory();
        }
    }

    private void GameVictory()
    {
        Time.timeScale = 0;
        scoreManager.CalcFinalScore();
        victoryPanel.SetActive(true);
    }
}
