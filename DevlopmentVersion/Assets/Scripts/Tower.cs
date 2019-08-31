/*
 * Tower class
 *
 * Responsible for managing the lifepoints of the tower and managing game over event when
 * lifepoints reach 0.
 *
 * Author: Martin Schuster 
 */

using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField] private int lifePoints = 3;
    public GameObject gameOverPanel;
    public Image[] life;

    private void Start()
    {
        foreach (var item in life)
        {
            item.material.color = Color.green;
        }
    }

    public int GetLifePoints()
    {
        return lifePoints;
    }

    public void TakeDamage(int value)
    {
        lifePoints -= value;
        if (lifePoints == 2)
        {
            life[2].material.color = Color.red;
        }

        if (lifePoints == 1)
        {
            life[2].material.color = Color.red;
            life[1].material.color = Color.red;
        }
        
        if (lifePoints <= 0)
        {
            foreach (var item in life)
            {
                item.material.color = Color.red;
            }
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
