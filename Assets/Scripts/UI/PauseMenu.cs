using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseCanvas;

    private bool isPaused = false;

    private void Awake()
    {
        Resume(); // Forcibly removing the pause
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }  
    }

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        isPaused = true;

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        isPaused = false;

        Time.timeScale = 1f;
    }

    public void Restart()
    {
        // TODO: Если это реально пофиксить малыми силами, то сделай это
        Resume(); // If not Resume, transition animation not start playing
        GameManager.Instance.Restart();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Go to Main Menu
    }
}
