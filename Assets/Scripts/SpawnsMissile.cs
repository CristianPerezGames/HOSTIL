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
            float timeInterval = Random.Range(0.3f, 2.0f);
            yield return new WaitForSecondsRealtime(timeInterval);
            Instantiate(missilePrefab, GetSpawner().position, Quaternion.identity);        
        }
    }

    Transform GetSpawner()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
