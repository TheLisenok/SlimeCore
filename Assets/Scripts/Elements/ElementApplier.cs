using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ОТДАЁТ элемент объекту
/// </summary>
public class ElementApplier : Interactable
{
    [Header("Element of this object")]
    [SerializeField] private ElementType ElementToInteract; // The element assigned to this object

    /// <summary>
    /// Handles interaction logic, passing the element to the slime.
    /// </summary>
    public override void OnInteract(GameObject interactor) // Логика интеракции в родителе
    {
        base.OnInteract();

        // TODO: честно, всё равно выглядит как костыль, НО так как у нас не один игрок, а целых 8, то хз

        // Проверка на игровой объект
        if (interactor.TryGetComponent<ElementHolder>(out ElementHolder elementHolder))
        {
            elementHolder.ReactWith(ElementManager.Instance.CreateElementFromType(ElementToInteract));
        }
        // Проверка на Слайма
        else if (interactor.TryGetComponent<SlimeDot>(out SlimeDot slimeDot))
        {
            slimeDot.RootHolder.ReactWith(ElementManager.Instance.CreateElementFromType(ElementToInteract));
        }

        //ElementManager.Instance.SetElementToSlime(ElementToInteract);
    }

    // TODO: добавь защиту того, чтобы не происходили бесконечные реакции
          // this источник НА ВРЕМЯ вырубается (это должно отображаться и визуально)
}
