using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A specialized button that reacts to elemental interactions.
/// Inherits from OnElementInteractable.
/// </summary>
public class ElementalButton : OnElementInteractable
{
    [SerializeField] private Door door; // Reference to the door this button controls

    public override void OnElementInteract()
    {
        base.OnElementInteract();
        door.Open();

    }
}
