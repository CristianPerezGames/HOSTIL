using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Menu,
    Playing,
    EndGame,
    Pause
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
        spawnsMissile.SpawnMissile();
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

    public void CheckStateGame()
    {
        if (VerifyGameEnd())
        {
            if (onChangeStateGame != null)
            {
                onChangeStateGame.Invoke(GameState.EndGame);
            }
        }
    }

    private void OnDisable()
    {
       
    }
}