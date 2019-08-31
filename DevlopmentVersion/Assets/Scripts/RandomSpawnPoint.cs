/*
 * RandomSpawnPoint class
 *
 * Returns a random point in a given bounding box (scaling of the object) around an object.
 * 
 * Author: Martin Schuster 
 */

using UnityEngine;

public class RandomSpawnPoint : MonoBehaviour
{
    public Color GizmoColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
    private Vector3 origin;
    private Vector3 relativeDistance;

    private void Start()
    {
        origin = transform.position;
        relativeDistance = transform.localScale / 2.0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    public Vector3 SpawnPoint()
    {
        var randomRange = new Vector3(Random.Range(-relativeDistance.x, relativeDistance.x),
            0, Random.Range(-relativeDistance.z, relativeDistance.z));
        var randomSpawnPoint = origin + randomRange;
        return randomSpawnPoint;
    }
}