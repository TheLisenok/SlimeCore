using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("level01"); // «апускаем первый уровень
    }

    public void LoadGame() // TODO: —делать систему сохранений дл€ последней локации
    {
        return;
    }
}
