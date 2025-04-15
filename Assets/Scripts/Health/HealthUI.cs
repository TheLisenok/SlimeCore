using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public static HealthUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    [Header("Исходный спрайт")]
    [SerializeField] private GameObject sourceHP;

    [Header("Настройки спавна")]
    [SerializeField] private RectTransform targetSpawn; // UI-объект, в котором будут появляться HP
    [SerializeField] private Vector2 offsetSpawn = new Vector2(50, 0); // Смещение между жизнями

    private List<GameObject> spawnSprites = new List<GameObject>();
    private Vector2 pastSpawn;

    private void Start()
    {
        pastSpawn = targetSpawn.position;
    }

    public void AddHP(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newHP = Instantiate(sourceHP, targetSpawn);
            RectTransform newHPRect = newHP.GetComponent<RectTransform>();

            if (newHPRect != null)
            {
                newHPRect.anchoredPosition = pastSpawn + offsetSpawn;
                pastSpawn = newHPRect.anchoredPosition;
            }

            spawnSprites.Add(newHP);
        }
    }

    public void RemoveHP(int count)
    {
        for (int i = 0; i < count && spawnSprites.Count > 0; i++)
        {
            GameObject lastHP = spawnSprites[spawnSprites.Count - 1];
            spawnSprites.RemoveAt(spawnSprites.Count - 1);
            Destroy(lastHP);
        }

        // Обновляем pastSpawn, чтобы следующий элемент появлялся в правильном месте
        if (spawnSprites.Count > 0)
            pastSpawn = spawnSprites[spawnSprites.Count - 1].GetComponent<RectTransform>().anchoredPosition;
        else
            pastSpawn = targetSpawn.anchoredPosition;
    }
}
