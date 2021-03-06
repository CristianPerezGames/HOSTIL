﻿using System.Collections;
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
    public AudioSource playAudioWave;

    public Coroutine rutineSpawn;

    public float timeWaitWave = 5;
    public float timeWave = 5;
    public int currWave = 1;

    public float currTimeWaitWave;
    public float currTimeWave;
    bool waitWave = false;
    bool waitWaveStart = false;

    bool firstWave = false;

    private void Start()
    {
        RoundManager.Instance.onChangeStateGame += StateGame;
        waitWave = true;
        waitWaveStart = true;
    }

    public void SpawnMissile()
    {
        if (!firstWave)
        {
            mainMusic.Play();
            InitWave();
            firstWave = true;
            return;
        }

        if (rutineSpawn == null)
        {
            rutineSpawn = StartCoroutine(RutineMissile());
        }
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

    void InitWave()
    {
        StartCoroutine(RutineWave());
    }

    IEnumerator RutineWave()
    {
        yield return new WaitForSeconds(1f);

        yield return RutineWaveStart();
        waitWave = false;
        timeWave += currWave * 0.5f;

        RoundManager.Instance.CallOnChangeGame(GameState.Playing);
    }

    IEnumerator RutineMissile()
    {
        while (true)
        {
            float timeInterval = Random.Range(0.3f, 1.2f);
            yield return new WaitForSecondsRealtime(timeInterval);
            int totalMissiles = Random.Range(1, Mathf.CeilToInt(1 + (currWave * 0.3f)));
            for (int i = 0; i < totalMissiles; i++)
            {
                var newMissile = Instantiate(missilePrefab, new Vector2(Random.Range(-6, 6), 7), Quaternion.identity);
                newMissile.SetTargetReactor(GetLiveReactor());
                yield return new WaitForSeconds(0.1f);
            }

        }
    }

    IEnumerator RutineWaveStart()
    {
        UIGameMaster.Instance.textStartRound.text = "WAVE START" + "\n<color=yellow> <size=100>" + currWave.ToString() + "</size> </color>";
        UIGameMaster.Instance.textStartRound.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        UIGameMaster.Instance.textStartRound.gameObject.SetActive(false);
        UIGameMaster.Instance.textCountRound.text = "3";
        playAudioWave.Play();
        for (int i = 3; i > 0; i--)
        {
            UIGameMaster.Instance.textCountRound.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            UIGameMaster.Instance.textCountRound.gameObject.SetActive(false);
            UIGameMaster.Instance.textCountRound.text = (i - 1).ToString();
        }
    }

    IEnumerator RutineEndWave()
    {
        UIGameMaster.Instance.textStartRound.gameObject.SetActive(true);
        UIGameMaster.Instance.textStartRound.text = "END WAVE";
        yield return new WaitForSeconds(2);
        UIGameMaster.Instance.textStartRound.gameObject.SetActive(false);
    }

    private GameObject GetLiveReactor()
    {
        List<GameObject> liveReactors = new List<GameObject>();
        if (!reactor1.GetComponent<ReactorEnergy>().IsBroken())
        {
            liveReactors.Add(reactor1);
        }
        if (!reactor2.GetComponent<ReactorEnergy>().IsBroken())
        {
            liveReactors.Add(reactor2);
        }
        if (!reactor3.GetComponent<ReactorEnergy>().IsBroken())
        {
            liveReactors.Add(reactor3);
        }

        if (liveReactors.Count > 0)
        {
            return liveReactors[Random.Range(0, liveReactors.Count)];
        }
        else
        {
            return null;
        }
    }

    public void Update()
    {
        if (RoundManager.Instance.gameState == GameState.PreGame || RoundManager.Instance.gameState == GameState.Pause || RoundManager.Instance.gameState == GameState.EndGame || RoundManager.Instance.gameState == GameState.MiniGame)
            return;

        if (!waitWave)
        {
            currTimeWave += Time.deltaTime;
            if (currTimeWave > timeWave)
            {
                currTimeWave = 0;
                waitWave = true;
                waitWaveStart = false;
                StopSpawnRutine();
                StartCoroutine(RutineEndWave());
                RoundManager.Instance.CallOnChangeGame(GameState.waitWave);
                currWave++;
            }
        }
        if (!waitWaveStart)
        {
            currTimeWaitWave += Time.deltaTime;
            if (currTimeWaitWave > timeWaitWave)
            {
                currTimeWaitWave = 0;
                waitWaveStart = true;
                InitWave();
            }
        }
    }

    public void PlayMusic()
    {
        if (isMusicPlaying)
        {
            return;
        }
        else
        {
            isMusicPlaying = true;
            mainMusic.Play();
        }
    }

    public void PauseMusic()
    {
        if (isMusicPlaying)
        {
            mainMusic.Pause();
            isMusicPlaying = false;
        }
    }

    public void StopMusic()
    {
        mainMusic.Stop();
        isMusicPlaying = false;
    }
}
