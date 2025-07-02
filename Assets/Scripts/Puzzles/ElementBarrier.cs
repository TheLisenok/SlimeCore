using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBarrier : OnElementInteractable
{
    [SerializeField] private Sprite elemptySprite; // TODO: Если найдёшь лучше реализацию, поменяй

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

            // TODO переделай эту хрень, пусть она не у слайма элемент меняет, а у объекта
            Slime.Instance.SetSlimeElementForced(ElementType.None); // "Забираем" стихию у слайма

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
