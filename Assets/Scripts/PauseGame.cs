using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    private void Start()
    {
        RoundManager.Instance.onChangeStateGame += ChangeState;
    }

    void ChangeState(GameState gameState)
    {
        if (gameState != GameState.Pause)
            this.gameObject.SetActive(false);
    }

    public void ContinueGame()
    {
        RoundManager.Instance.CallOnChangeGame(GameState.Playing);
        gameObject.SetActive(false);
    }

    public void ReturnHome()
    {
        RoundManager.Instance.StopSlowMotion();
        SceneController.Instance.ChangeScene(1);
    }
}
