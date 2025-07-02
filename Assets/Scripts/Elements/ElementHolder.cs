using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The status of the Element for the object
/// </summary>
public class ElementHolder : MonoBehaviour
{
    [SerializeField] private ElementType currentType = ElementType.None;
    protected Element currentElement;

    public Element CurrentElement => currentElement; // Get current element

    protected virtual void Awake()
    {
        currentElement = ElementManager.Instance.CreateElementFromType(currentType);
    }

    public void SetElementForced(Element newElement)
    {
        currentElement = newElement;
        currentType = newElement.Type;
        currentElement.ApplyEffect(this);
    }

    public void ReactWith(Element incomingElement)
    {
        Element result = currentElement.ReactWith(incomingElement);
        SetElementForced(result);
    }

    // 
    public virtual void ApplyEffect(Element element)
    {
        element.ApplyEffect(this);
    }
}
