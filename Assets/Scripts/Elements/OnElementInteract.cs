using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// ПОЛУЧАЕТ ЭЛЕМЕНТ и проверяет соответствие
/// </summary>
public class OnElementInteractable : Interactable
{
    [SerializeField] protected ElementType elementToInteract;

    public virtual void OnElementInteract()
    {
        //Debug.Log($"Сработала интеракция");
    }

    public override void OnInteract(GameObject interactor)
    {
        base.OnInteract(interactor);

        // TODO: тоже выглядит как костыль
        // Для объектов 
        if (interactor.TryGetComponent<ElementHolder>(out ElementHolder elementHolder))
        {
            Debug.Log("Происходит интеракция элементов");

            if (elementHolder.CurrentElement.Type == elementToInteract)
            {
                OnElementInteract();
            }
        }
        // Для Слайма
        else if (interactor.TryGetComponent<SlimeDot>(out SlimeDot slimeDot))
        {
            if (slimeDot.RootHolder.CurrentElement.Type == elementToInteract)
            {
                OnElementInteract();
            }
        }
    }
}

