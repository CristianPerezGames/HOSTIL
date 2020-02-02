using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public void OnClickPlay() 
    {
        SceneController.Instance.ChangeScene(0);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
