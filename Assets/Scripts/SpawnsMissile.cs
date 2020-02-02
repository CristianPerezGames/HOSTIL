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

    private bool isMusicPlaying = false;
    public AudioSource mainMusic;

    public Coroutine rutineSpawn;

    private void Start()
    {
        RoundManager.Instance.onChangeStateGame += StateGame;
    }

    public void SpawnMissile()
    {
        if(rutineSpawn == null)
            rutineSpawn = StartCoroutine(RutineMissile());
    }

    void StopSpawnRutine()
    {
        if (rutineSpawn != null)
        {
            StopCoroutine(rutineSpawn);
            rutineSpawn = null;
        }
    }

    void StateGame(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.Playing:
                SpawnMissile();
                PlayMusic();
                break;
            case GameState.EndGame:
                StopSpawnRutine();
                StopMusic();
                break;
            case GameState.Pause:
                StopSpawnRutine();
                PauseMusic();
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
            var newMissile = Instantiate(missilePrefab, new Vector2(Random.Range(-6,6),7), Quaternion.identity);
            newMissile.SetTargetReactor(GetLiveReactor());        
        }
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

    public void PlayMusic(){
        if (isMusicPlaying){
            return;
        } else {
            isMusicPlaying = true;
            mainMusic.Play();
            }
    }

    public void PauseMusic(){
        if (isMusicPlaying){
            mainMusic.Pause();
            isMusicPlaying = false;
        }
    }

    public void StopMusic(){
        mainMusic.Stop();
        isMusicPlaying = false;
    }
}
