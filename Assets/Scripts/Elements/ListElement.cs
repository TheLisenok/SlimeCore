using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.CoreUtils;

/// <summary>
/// Represents an empty (undefined) element.
/// </summary>
public class EmptyElement : Element
{
    public EmptyElement(ElementData elementData) : base(elementData) { }

    public override void ApplyEffect(Slime slime)
    {
        Debug.Log("Slime is empty (this shouldn't be possible...)");
    }
    public override Element ReactWith(Element otherElement)
    {
        Debug.Log($"Empty reacts with {otherElement.Name}, now the slime is this.");
        return otherElement;
    }
}

// =============================================================================================================================
/// <summary>
/// Fire element and its interactions.
/// </summary>
public class FireElement : Element
{
    private static readonly Dictionary<ElementType, Func<FireElement, Element>> _reactions = new()
    {
        { ElementType.Water, fire => {
            ReactionEffect.Instance.SetWindZone();
            EmotionManager.Instance.SetSurprise();
            Debug.Log("Fire reacts with Water: Updraft is created.");
            return new AirElement(fire.GetElementData(ElementType.Air));
        } },
        { ElementType.Air, fire => {
            ReactionEffect.Instance.SetFireWhirlwind();
            Debug.Log("Fire reacts with Air: Fire whirlwind appears.");
            return new FireElement(fire.GetElementData(ElementType.Fire));
        } },
        { ElementType.Ice, fire => {
            Debug.LogWarning("Fire reacts with Ice: slime is now water-based");
            // TODO: Create steam push
            return new WaterElement(fire.GetElementData(ElementType.Water));
        } },
        { ElementType.Electricity, fire => {
            ReactionEffect.Instance.SetCaboom();
            EmotionManager.Instance.SetUhgh();
            Debug.Log("Fire reacts with Electricity: Explosion occurs.");
            return new FireElement(fire.GetElementData(ElementType.Fire));
        } }
    };
    
    public FireElement(ElementData elementData) : base(elementData) { }

    public override void ApplyEffect(Slime slime)
    {
        Debug.Log("Slime is now fiery.");
    }

    public override Element ReactWith(Element otherElement)
    {
        if (_reactions.TryGetValue(otherElement.Type, out var reaction))
        {
            return reaction(this);
        }
        return otherElement;
    }
}

// =============================================================================================================================
/// <summary>
/// Water element and its interactions.
/// </summary>
public class WaterElement : Element
{
    private static readonly Dictionary<ElementType, Func<WaterElement, Element>> _reactions = new()
    {
        { ElementType.Fire, water => {
            ReactionEffect.Instance.SetWindZone();
            EmotionManager.Instance.SetSurprise();
            Debug.Log("Water reacts with Fire: Updraft is created.");
            return new AirElement(water.GetElementData(ElementType.Air));
        }},
        { ElementType.Air, water => {
            Debug.Log("Water reacts with Air: Tornado created.");
            return new WaterElement(water.GetElementData(ElementType.Water));
        }},
        { ElementType.Ice, water => {
            Debug.Log("Water reacts with Ice: Make Iced");
            return new IceElement(water.GetElementData(ElementType.Ice));
        }},
        { ElementType.Electricity, water => {
            Debug.Log("Water reacts with Electricity: No Reaction");
            return new WaterElement(water.GetElementData(ElementType.Water));
        }}
    };
    public WaterElement(ElementData elementData) : base(elementData) { }

    public override void ApplyEffect(Slime slime)
    {
        Debug.Log("Slime is now water-based.");
    }

    public override Element ReactWith(Element otherElement)
    {
        if (_reactions.TryGetValue(otherElement.Type, out var reaction))
        {
            return reaction(this);
        }
        return otherElement;
    }
}

// =============================================================================================================================
/// <summary>
/// Air element and its interactions.
/// </summary>
public class AirElement : Element
{
    private static readonly Dictionary<ElementType, Func<AirElement, Element>> _reactions = new()
    {
        { ElementType.Fire, air => {
            ReactionEffect.Instance.SetFireWhirlwind();
            Debug.Log("Air reacts with Fire: Fire whirlwind appears.");
            return new FireElement(air.GetElementData(ElementType.Fire));
        }},
        { ElementType.Water, air => {
            Debug.Log("Air reacts with Water: Mini tornado created.");
            return new WaterElement(air.GetElementData(ElementType.Water));
        }},
        { ElementType.Ice, air => {
            Debug.Log("Air reacts with Ice: No Reaction");
            return new IceElement(air.GetElementData(ElementType.Ice));
        }},
        { ElementType.Electricity, air => {
            Debug.Log("Air reacts with Electricity: No reaction");
            return new ElectricityElement(air.GetElementData(ElementType.Electricity));
        }}
    };
    public AirElement(ElementData elementData) : base(elementData) { }

    public override void ApplyEffect(Slime slime)
    {
        Debug.Log("Слайм воздушный");
    }

    public override Element ReactWith(Element otherElement)
    {
        if (_reactions.TryGetValue(otherElement.Type, out var reaction))
        {
            return reaction(this);
        }
        return otherElement;
    }
}

// =============================================================================================================================
/// <summary>
/// Ice element and its interactions.
/// </summary>
public class IceElement : Element
{
    private static readonly Dictionary<ElementType, Func<IceElement, Element>> _reactions = new()
    {
        { ElementType.Fire, ice => {
            Debug.Log("Ice reacts with Fire: Slime becomes water-based.");
            // TODO: Add steam push
            return new WaterElement(ice.GetElementData(ElementType.Water));
        }},
        { ElementType.Water, ice => {
            Debug.Log("Ice reacts with Water: Ice crust forms on water.");
            // TODO: Add platform effect
            return new IceElement(ice.GetElementData(ElementType.Ice));
        }},
        { ElementType.Air, ice => {
            Debug.Log("Ice reacts with Air: Snow storm");
            return new IceElement(ice.GetElementData(ElementType.Ice));
        }},
        { ElementType.Electricity, ice => {
            Debug.Log("Ice reacts with Electricity: No Reaction");
            return new IceElement(ice.GetElementData(ElementType.Ice));
        }}
    };
    public IceElement(ElementData elementData) : base(elementData) { }

    public override void ApplyEffect(Slime slime)
    {
        Debug.Log("Слайм ледяной");
    }

    public override Element ReactWith(Element otherElement)
    {
        if (_reactions.TryGetValue(otherElement.Type, out var reaction))
        {
            return reaction(this);
        }
        return otherElement;
    }
}

// =============================================================================================================================
/// <summary>
/// Electricity element and its interactions.
/// </summary>
public class ElectricityElement : Element
{
    private static readonly Dictionary<ElementType, Func<ElectricityElement, Element>> _reactions = new()
    {
        { ElementType.Fire, electricity => {
            ReactionEffect.Instance.SetCaboom();
            EmotionManager.Instance.SetUhgh();
            Debug.Log("Electricity reacts with Fire: Explosion occurs.");
            return new FireElement(electricity.GetElementData(ElementType.Fire));
        }},
        { ElementType.Water, electricity => {
            Debug.Log("Electricity reacts with Water: No Reaction");
            return new WaterElement(electricity.GetElementData(ElementType.Water));
        }},
        { ElementType.Air, electricity => {
            Debug.Log("Electricity reacts with Air: No Reaction");
            return new ElectricityElement(electricity.GetElementData(ElementType.Electricity));
        }},
        { ElementType.Ice, electricity => {
            Debug.Log("Electricity reacts with Ice: No Reaction");
            return new ElectricityElement(electricity.GetElementData(ElementType.Electricity));
        }}
    };

    public ElectricityElement(ElementData elementData) : base(elementData) { }

    public override void ApplyEffect(Slime slime)
    {
        Debug.Log("Слайм электрический");
    }

    public override Element ReactWith(Element otherElement)
    {
        if (_reactions.TryGetValue(otherElement.Type, out var reaction))
        {
            return reaction(this);
        }
        return otherElement;
    }
}