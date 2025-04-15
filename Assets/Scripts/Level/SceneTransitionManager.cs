using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    #region Singleton 
    // TODO: ��������� ��� Singleton ��� ����� ������
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
        transitionAnimator.Play("Open"); // ��������� �������� ��� ��������� �� ������
    }

    // ����� ��� ������� ��������
    public void StartTransition(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // �������� �������� ����� � ����
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // ��������� �������� ��������
        transitionAnimator.Play("Close");

        // ����������� �������� �����
        asyncLoadOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncLoadOperation.allowSceneActivation = false; // �� ���������� ����� �����

        // ���, ���� ����� ���������� ���� �� �� 90%
        while (asyncLoadOperation.progress < 0.9f)
        {
            yield return null;
        }

        // ��������, ��� ����� ������
        isSceneReady = true;
    }

    // ���� ����� ���������� �� ������������� ������� � ����� ��������
    public void OnTransitionComplete()
    {
        if (isSceneReady)
        {
            asyncLoadOperation.allowSceneActivation = true; // ���������� �����
        }
    }
}
