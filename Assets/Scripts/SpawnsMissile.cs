using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsMissile : MonoBehaviour
{
    public MissileController missilePrefab;
    public List<Transform> spawnPoints;

    public Coroutine rutineSpawn;

    private void Start()
    {
        RoundManager.Instance.onChangeStateGame += StateGame;
    }

    public void SpawnMissile()
    {
        rutineSpawn = StartCoroutine(RutineMissile());
    }

    void StopSpawnRutine()
    {
        if (rutineSpawn != null)
        {
            StopCoroutine(rutineSpawn);
        }
    }

    void StateGame(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.Playing:
                SpawnMissile();
                break;
            case GameState.EndGame:
                StopSpawnRutine();
                break;
            case GameState.Pause:
                break;
            case GameState.MiniGame:
                StopSpawnRutine();
                break;
        }
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
