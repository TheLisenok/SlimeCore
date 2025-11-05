using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singletone
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Debug   TODO: Убери при релизе
        {
            Restart();
        }
    }

    public void NextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneTransitionManager.Instance.StartTransition(
            SceneUtility.GetScenePathByBuildIndex(levelIndex));
    }
    public void LoadLevel(string levelName) // Перегрузка метода
    {
        SceneTransitionManager.Instance.StartTransition(levelName);
    }

    public void Restart()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartWithDelay(float delay = 1f)
    {
        StartCoroutine(IERestartDelay(delay));
    }

    private IEnumerator IERestartDelay(float time = 1f)
    {
        yield return new WaitForSeconds(time);
        
        Restart();
    }
}
