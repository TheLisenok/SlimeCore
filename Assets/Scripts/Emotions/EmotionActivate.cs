using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionActivate : Interactable
{
    [Header("Какую эмоцию вызвать?")]
    [SerializeField] private EmotionType type;

    public override void OnInteract()
    {
        base.OnInteract();

        EmotionManager.Instance.ActivateEmotion(type);
    }
}
