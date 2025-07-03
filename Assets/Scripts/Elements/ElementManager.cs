using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the different elements and assigns them to the slime.
/// Implements a singleton pattern.
/// </summary>
public class ElementManager : MonoBehaviour
{
    #region Singletone
    public static ElementManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    #endregion

    [SerializeField] private ElementData emptyElement;
    [SerializeField] private ElementData fireElement;
    [SerializeField] private ElementData waterElement;
    [SerializeField] private ElementData airElement;
    [SerializeField] private ElementData IceElement;
    [SerializeField] private ElementData ElectricityElement;

    /// <summary>
    /// Assigns an element to the slime.
    /// </summary>
    /// <param name="elementType">The type of element to assign.</param>
    public void SetElementToSlime(ElementType elementType)
    {
        if (Slime.Instance.CurrentElement != null)
        {
            Element newElement = CreateElementFromType(elementType);
            Slime.Instance.ChangeElement(newElement); // Передаём сам Element
        }
    }

    /// <summary>
    /// Creates an element instance based on the given ElementType.
    /// </summary>
    /// <param name="type">The type of element to create.</param>
    /// <returns>A new instance of the corresponding element.</returns>
    public Element CreateElementFromType(ElementType type)
    {
        switch (type)
        {
            case ElementType.Fire: return new FireElement(fireElement);
            case ElementType.Water: return new WaterElement(waterElement);
            case ElementType.Air: return new AirElement(airElement);
            case ElementType.Ice: return new IceElement(IceElement);
            case ElementType.Electricity: return new ElectricityElement(ElectricityElement);
            case ElementType.None: return new EmptyElement(emptyElement);
            default: return new EmptyElement(emptyElement);
        }
    }

    /// <summary>
    /// Retrieves the ElementData associated with a specific ElementType.
    /// </summary>
    /// <param name="elementType">The type of element.</param>
    /// <returns>The corresponding ElementData</returns>
    public ElementData GetElementData(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.Fire: return fireElement;
            case ElementType.Water: return waterElement;
            case ElementType.Air: return airElement;
            case ElementType.Ice: return IceElement;
            case ElementType.Electricity: return ElectricityElement;
            default: return emptyElement;
        }
    }

}
