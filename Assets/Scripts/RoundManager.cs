using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Menu,
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

    public List<ReactorController> reactorControllers;

    private void Awake()
    {

    }

    private void Start()
    {
        StartCoroutine(RutineStartGame());
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