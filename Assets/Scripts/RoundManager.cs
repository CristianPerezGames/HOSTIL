using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    PreGame,
    Playing,
    EndGame,
    Pause,
    MiniGame
}

public class RoundManager : Singleton<RoundManager>
{
    public Action<GameState> onChangeStateGame;

    public GameState gameState = GameState.Playing;
    
    public SpawnsMissile spawnsMissile;
    public PlayerController playerController;

    public List<ReactorController> reactorControllers;

    GameState storeState = GameState.Playing;

    private Coroutine slowPause;

    private void Awake()
    {
        onChangeStateGame += StateRound;
    }

    private void Start()
    {
        StartCoroutine(RutineStartGame());
    }

    void StateRound(GameState _gameState)
    {
        if(_gameState == GameState.Pause)
            StarSlowPause();
        if (_gameState == GameState.MiniGame)
            Time.timeScale = 0;
        if (_gameState == GameState.Playing)
        {
            StopSlowMotion();
            Time.timeScale = 1;
        }
    }

    public void StopSlowMotion()
    {
        if (slowPause != null)
        {
            StopCoroutine(slowPause);
            slowPause = null;
        }
    }

    void StarSlowPause()
    {
        if(slowPause == null)
            slowPause = StartCoroutine(RutineSlowPause());
    }

    IEnumerator RutineSlowPause()
    {
        float tim = 1;
        while (tim > 0)
        {
            tim -= Time.deltaTime * 4;
            Time.timeScale = tim;
            yield return null;
        }

        Time.timeScale = 0;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState != GameState.Pause)
            {
                storeState = gameState;
                CallOnChangeGame(GameState.Pause);
            }
            else 
            {
                CallOnChangeGame(storeState);
            }
        }
    }

    IEnumerator RutineStartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        CallOnChangeGame(GameState.Playing);
    }

    bool VerifyGameEnd()
    {
        foreach (var rCont in reactorControllers)
        {
            if (rCont.reactorState == ReactorState.Alive)
            {
                return false;
            }
        }
        return true;
    }


    public void CallOnChangeGame(GameState _gameState)
    {
        gameState = _gameState;
        if (onChangeStateGame != null)
        {
            onChangeStateGame.Invoke(_gameState);
        }
    }

    public void CheckStateGame()
    {
        if (VerifyGameEnd())
        {
            CallOnChangeGame(GameState.EndGame);
        }
    }

    private void OnDisable()
    {

    }
}