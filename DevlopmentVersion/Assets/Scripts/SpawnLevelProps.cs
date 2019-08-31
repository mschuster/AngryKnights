/*
 * SpawnLevelProbs class
 *
 * Spawns random objects in the gameworld from a given list of objects.
 * 
 * Author: Martin Schuster 
 */

using System.Collections.Generic;
using UnityEngine;

public class SpawnLevelProps : MonoBehaviour
{
    public Color GizmoColor = new Color(0.8f, 0.8f, 0.8f, 0.4f);
    public GameObject levelProbs;
    private Vector3 origin;
    [SerializeField] private int probAmount = 100;
    [SerializeField] private List<GameObject> probList;

    private Vector3 relativeDistance;

    private void Start()
    {
        levelProbs = GameObject.FindGameObjectWithTag("Probs");
        var objTransform = transform;
        origin = objTransform.position;
        relativeDistance = objTransform.localScale / 2.0f;
        var probs = probList.Count;
        int probIndex;
        for (var i = 0; i < probAmount; i++)
        {
            probIndex = Random.Range(0, probs);
            Instantiate(probList[probIndex], SpawnPoint(), Quaternion.Euler(0, Random.Range(0, 360), 0),
                levelProbs.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    public Vector3 SpawnPoint()
    {
        var randomRange = new Vector3(Random.Range(-relativeDistance.x, relativeDistance.x),
            0,
            Random.Range(-relativeDistance.z, relativeDistance.z));
        var randomSpawnPoint = origin + randomRange;
        return randomSpawnPoint;
    }
}