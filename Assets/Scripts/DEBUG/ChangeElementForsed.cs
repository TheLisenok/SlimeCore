using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeElementForsed : MonoBehaviour
{
    public void SetFire()
    {
        Slime.Instance.SetSlimeElementForced(ElementType.Fire);
    }
    public void SetWater()
    {
        Slime.Instance.SetSlimeElementForced(ElementType.Water);
    }
    public void SetAir()
    {
        Slime.Instance.SetSlimeElementForced(ElementType.Air);
    }
    public void SetIce()
    {
        Slime.Instance.SetSlimeElementForced(ElementType.Ice);
    }
    public void SetElectro()
    {
        Slime.Instance.SetSlimeElementForced(ElementType.Electricity);
    }
    public void SetEmpty()
    {
        Slime.Instance.SetSlimeElementForced(ElementType.None);
    }
}
