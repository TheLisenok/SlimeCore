using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    #region Singleton 
    // TODO: переделай все Singleton под ТАКОЙ шаблон
    public static SceneTransitionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Animator transitionAnimator;
    private AsyncOperation asyncLoadOperation;
    private bool isSceneReady = false;

    private void Start()
    {
        transitionAnimator.Play("Open"); // Запускаем анимацию при появлении на уровне
    }

    // Метод для запуска перехода
    public void StartTransition(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Корутина загрузки сцены в фоне
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Запускаем анимацию перехода
        transitionAnimator.Play("Close");

        // Асинхронная загрузка сцены
        asyncLoadOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncLoadOperation.allowSceneActivation = false; // Не активируем сцену сразу

        // Ждём, пока сцена загрузится хотя бы на 90%
        while (asyncLoadOperation.progress < 0.9f)
        {
            yield return null;
        }

        // Помечаем, что сцена готова
        isSceneReady = true;
    }

    // Этот метод вызывается из анимационного события в конце анимации
    public void OnTransitionComplete()
    {
        if (isSceneReady)
        {
            asyncLoadOperation.allowSceneActivation = true; // Активируем сцену
        }
    }
}
