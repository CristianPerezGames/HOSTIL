using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBase : MonoBehaviour
{
    public void Close()
    {
        RoundManager.Instance.CallOnChangeGame(GameState.Playing);
        Destroy(gameObject);
    }
}
