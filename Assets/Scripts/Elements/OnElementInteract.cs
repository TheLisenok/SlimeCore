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

        // TODO: Если будет можно, то исправь эту ересь
        if (interactor.CompareTag("Slime"))
        {
            Debug.Log("Это игрок интерактирует с элементом");

            if (Slime.Instance.getCurrentElement() == elementToInteract)
            {
                OnElementInteract();
            }
        }
    }
}

