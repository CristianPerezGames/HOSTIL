using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsMissile : MonoBehaviour
{
    public MissileController missilePrefab;
    public List<Transform> spawnPoints;

    public GameObject reactor1;
    public GameObject reactor2;
    public GameObject reactor3;

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
        }
    }

    IEnumerator RutineMissile()
    {
        while (true)
        {
            float timeInterval = Random.Range(0.3f, 2.0f);
            yield return new WaitForSecondsRealtime(timeInterval);
            var newMissile = Instantiate(missilePrefab, new Vector2(Random.Range(-6,6),7), Quaternion.identity);
            newMissile.SetTargetReactor(GetLiveReactor());        
        }
    }

    Transform GetSpawner()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    private GameObject GetLiveReactor(){
        List<GameObject> liveReactors = new List<GameObject>();
        if(!reactor1.GetComponent<ReactorEnergy>().IsBroken()){
            liveReactors.Add(reactor1);
        }
        if(!reactor2.GetComponent<ReactorEnergy>().IsBroken()){
            liveReactors.Add(reactor2);
        }
        if(!reactor3.GetComponent<ReactorEnergy>().IsBroken()){
            liveReactors.Add(reactor3);
        }

        if (liveReactors.Count>0){
            return liveReactors[Random.Range(0, liveReactors.Count)];
        } else {
            return null;
        }        
    }
}
