using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EmotionType { None, Dead, Disappoint, Happy, Surprise, Uhgh, Confuse }

public class EmotionManager : MonoBehaviour
{
    #region Singletone
    public static EmotionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    #endregion

    [SerializeField] private Vector3 offsetSpawnWindow;
    [SerializeField] private GameObject emotionCanvas;
    [SerializeField] private Image emotionImagePlace;
    [SerializeField] private Animator emotionAnimator;

    [Header("Картинки с эмоциями")] // Если будет слишком много, замени на список или словарь
    [SerializeField] private Sprite dead;
    [SerializeField] private Sprite disappoint;
    [SerializeField] private Sprite happy;
    [SerializeField] private Sprite surprise;
    [SerializeField] private Sprite uhgh;
    [SerializeField] private Sprite confuse;

    private bool isShowing = false;

    private void Start()
    {
        emotionCanvas.SetActive(false);
    }

    // Интерфейсы
    // TODO: Переделай через словарь
    public void ActivateEmotion(EmotionType type)
    {
        switch (type)
        {
            case EmotionType.Dead:
                SetDead();
                break;

            case EmotionType.Disappoint:
                SetDisappoint(); 
                break;

            case EmotionType.Happy:
                SetHappy();
                break;

            case EmotionType.Surprise:
                SetSurprise(); 
                break;

            case EmotionType.Uhgh:
                SetUhgh(); 
                break;

            case EmotionType.Confuse:
                SetConfuse();
                break;
        }
    }

    public void SetDead()
    {
        SetEmotion(dead);
    }
    public void SetDisappoint()
    {
        SetEmotion(disappoint);
    }
    public void SetHappy()
    {
        SetEmotion(happy);
    }
    public void SetSurprise()
    {
        SetEmotion(surprise);
    }
    public void SetUhgh()
    {
        SetEmotion(uhgh);
    }

    public void SetConfuse()
    {
        SetEmotion(confuse);
    }

    private void SetEmotion(Sprite sprite)
    {
        if (!isShowing)
        {
            Debug.Log("Активируем эмоцию " + sprite.name);
            
            // Телепортируем канвас к игроку
            emotionCanvas.transform.position = Slime.Instance.gameObject.transform.position + offsetSpawnWindow;

            isShowing = true;

            emotionCanvas.SetActive(true);
            emotionAnimator.SetTrigger("ShowEmotionWindow");

            emotionImagePlace.sprite = sprite;

            StartCoroutine(WaitAndDisable(2f));
        }
        else
        {
            Debug.LogWarning($"Анимация {sprite.name} уже показывается!");
        }
    }

    private IEnumerator WaitAndDisable(float time = 1f)
    {
        yield return new WaitForSeconds(time);

        emotionAnimator.SetTrigger("CloseEmotionWindow");

        yield return new WaitForSeconds(0.15f); // Время анимации

        emotionCanvas.SetActive(false);
        isShowing = false;
    }
}
