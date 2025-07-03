using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDot : MonoBehaviour
{
    public ElementHolder RootHolder; // Ссылка на состояние элемента

    private void Awake()
    {
        if (RootHolder == null)
        {
            Debug.LogWarning($"У {gameObject.name} НЕ задан ElementHolder!");
        }
    }
}
