﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameMaster : Singleton<UIGameMaster>
{
    public GameObject uiDeathGame;

    private void Start()
    {
        RoundManager.Instance.onChangeStateGame += UIStateGame;
    }

    void UIStateGame(GameState _state)
    {
        switch (_state)
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
                break;
            case GameState.EndGame:
                ShowDeathGame();
                break;
            case GameState.Pause:
                break;
        }
    }

    public void ShowDeathGame()
    {
        uiDeathGame.SetActive(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        
    }
}