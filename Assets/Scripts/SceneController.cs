using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public Image imageScene;

    public bool fadeOn;
    private void Awake()
    {
        if (SceneController.Instance != this)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);

        fadeOn = false;
    }

    public void ChangeScene(int indexScene)
    {
        fadeOn = true;
        StartCoroutine(RutineChangeScene(indexScene));
    }

    IEnumerator RutineChangeScene(int index)
    {
        yield return new WaitForSecondsRealtime(1.2f);
        SceneManager.LoadScene(index);
    }

    private void Update()
    {
        if (fadeOn)
        {
            Time.timeScale = 1;
            imageScene.CrossFadeAlpha(1, 0.3f, false);
        }
        else
        {
            imageScene.CrossFadeAlpha(0, 1, false);
        }

    }

    private void OnLevelWasLoaded(int level)
    {
        fadeOn = false;
    }
}
