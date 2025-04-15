using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBarrier : OnElementInteractable
{
    [SerializeField] private Sprite elemptySprite; // TODO: ���� ������ ����� ����������, �������

    private bool isOpen = false;

    private void Start()
    {
        BarrierOn();
        isOpen = false;
    }

    public override void OnElementInteract()
    {
        if (!isOpen)
        {
            base.OnElementInteract();

            BarrierOff();
            Slime.Instance.SetElementForced(ElementType.None); // "��������" ������ � ������

            isOpen = true;
        }
    }

    private void BarrierOff()
    {
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        
        this.gameObject.GetComponent<SpriteRenderer>().sprite = elemptySprite;

    }
    private void BarrierOn()
    {

    }
}
