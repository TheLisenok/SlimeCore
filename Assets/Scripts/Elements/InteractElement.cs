using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows interaction with an object to transfer an element to the slime.
/// </summary>
public class InteractElement : Interactable
{
    [Header("Element of this object")]
    [SerializeField] private ElementType ElementToInteract; // The element assigned to this object

    /// <summary>
    /// Handles interaction logic, passing the element to the slime.
    /// </summary>
    public override void OnInteract() // Логика интеракции в родителе
    {
        base.OnInteract();

        ElementManager.Instance.SetElementToSlime(ElementToInteract);
    }

    // TODO: добавь защиту того, чтобы не происходили бесконечные реакции
          // this источник НА ВРЕМЯ вырубается (это должно отображаться и визуально)
}
