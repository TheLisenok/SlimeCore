using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class OnElementInteractable : Interactable
{
    [SerializeField] protected ElementType elementToInteract;

    public virtual void OnElementInteract()
    {
        //Debug.Log($"��������� ����������");
    }

    public override void OnInteract(GameObject interactor)
    {
        base.OnInteract(interactor);

        // TODO: ���� ����� �����, �� ������� ��� �����
        if (interactor.CompareTag("Slime"))
        {
            Debug.Log("��� ����� ������������� � ���������");

            if (Slime.Instance.getCurrentElement() == elementToInteract)
            {
                OnElementInteract();
            }
        }
    }
}

