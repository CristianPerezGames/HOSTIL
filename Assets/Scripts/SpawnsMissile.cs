using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsMissile : MonoBehaviour
{
    public MissileController missilePrefab;
    public List<Transform> spawnPoints;

    public void SpawnMissile()
    {
        StartCoroutine(RutineMissile());
    }

    IEnumerator RutineMissile()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            Instantiate(missilePrefab, GetSpawner().position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(1);
        }
    }

    Transform GetSpawner()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
