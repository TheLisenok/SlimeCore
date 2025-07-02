using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

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

        if (interactor.TryGetComponent<ElementHolder>(out ElementHolder elementHolder))
        {
            Debug.Log("Происходит интеракция элементов");

            if (elementHolder.CurrentElement.Type == elementToInteract)
            {
                OnElementInteract();
            }
        }
    }
}

