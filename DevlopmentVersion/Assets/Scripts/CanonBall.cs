/*
 * Canonball class
 *
 * Responsible for creating the trajectory path of the cannonball.
 * Saves info how many enemy's were eliminated by that cannonball.
 * 
 * Author: Martin Schuster
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    private bool flying;
    public int multi = 1;
    private Vector3 oldPos;
    [SerializeField] private GameObject trajectorieObject;
    private Queue<GameObject> trajectoriePath;

    private void Start()
    {
        trajectoriePath = new Queue<GameObject>();
        flying = true;
        multi = 1;
    }

    private void Update()
    {
        if (flying)
        {
            oldPos = transform.position;
            trajectoriePath.Enqueue(Instantiate(trajectorieObject, oldPos, Quaternion.identity));
        }

        if (trajectoriePath.Count > 0 && !flying)
        {
            Destroy(trajectoriePath.First());
            trajectoriePath.Dequeue();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        flying = false;
        StartCoroutine(KillObject());
    }

    private IEnumerator KillObject()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}