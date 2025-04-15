using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Element/Create New Element")]
public class ElementData : ScriptableObject
{
    public ElementType Type;
    public string ElementName;
    public Texture2D SlimeTexture;
    public Color SlimeEdgeColor;
    public float SoftnessCoefficient = 1f;
}