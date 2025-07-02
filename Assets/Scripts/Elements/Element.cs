using UnityEngine;

/// <summary>
/// Defines different elemental types.
/// </summary>
public enum ElementType { None, Fire, Water, Ice, Air, Electricity }

/// <summary>
/// Base class for elements, providing core properties and behavior.
/// </summary>
public abstract class Element
{
    protected ElementData data;
    public ElementData Data => data; // Provides access to ElementData

    public ElementType Type => data.Type;
    public string Name => data.ElementName;
    public Texture2D SlimeTexture => data.SlimeTexture;
    public Color SlimeEdgeColor => data.SlimeEdgeColor;

    /// <summary>
    /// Constructor that initializes the element with its data.
    /// </summary>
    /// <param name="elementData">The data associated with this element.</param>
    public Element(ElementData elementData)
    {
        data = elementData;
    }

    /// <summary>
    /// Applies the element's effect to the slime.
    /// </summary>
    /// <param name="slime">The slime that the effect will be applied to.</param>
    public abstract void ApplyEffect(Slime slime);

    /// <summary>
    /// Reactions of elements of all objects EXPECT the Slime
    /// </summary>
    /// <param name="holder">The universal state class of the element</param>
    public abstract void ApplyEffect(ElementHolder holder);

    /// <summary>
    /// Defines how this element reacts with another element.
    /// Returns a new element resulting from the reaction.
    /// </summary>
    /// <param name="otherElement">The other element involved in the reaction.</param>
    /// <returns>The resulting element after the reaction.</returns>
    public abstract Element ReactWith(Element otherElement); // Return Element of Slime after reaction
    /// <summary>
    /// Retrieves the ElementData associated with a given ElementType.
    /// </summary>
    /// <param name="type">The type of element.</param>
    /// <returns>The corresponding ElementData.</returns>
    protected ElementData GetElementData(ElementType type)
    {
        return ElementManager.Instance.GetElementData(type);
    }
}
