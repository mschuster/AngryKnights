/**
 * Agent Class
 *
 * Sets the goal for the Enemy and checks if enemy is hit by a cannonball
 * or if it reached the goal.
 * 
 * Author: Martin Schuster
 */

using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public Transform goal;
    public ScoreManager scoreManager;
    public WaveManager waveManager;
    public int value;
    public EnemyType enemyType;
    
    private void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<WaveManager>();
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        goal = GameObject.FindGameObjectWithTag("Target").transform;
        GetComponent<NavMeshAgent>().enabled = true;
        var agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.position);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, goal.position) < 1.0f)
        {
            DamageTower();
        }
    }

    private void DamageTower()
    {
        var damage = GetDamageValue();
        goal.GetComponent<Tower>().TakeDamage(damage);
        waveManager.DecreaseRemainingUnits();
        Destroy(gameObject);
    }

    private int GetDamageValue()
    {
        switch (enemyType)
        {
            case EnemyType.Soldier:
                return 1;
            case EnemyType.Ram:
                return 2;
            default:
                return 0;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var impact = other.relativeVelocity.magnitude;
        if (other.gameObject.CompareTag("CanonBall") && impact > 5f)
        {
            var canonBall = other.gameObject.GetComponent<CanonBall>();
            scoreManager.IncreaseScore(canonBall.multi * value);
            scoreManager.IncreaseKills();
            canonBall.multi++;
            waveManager.DecreaseRemainingUnits();
            Destroy(gameObject);
        }
    }
}
