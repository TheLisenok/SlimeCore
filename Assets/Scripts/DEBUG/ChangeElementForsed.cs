using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeElementForsed : MonoBehaviour
{
    public void SetFire()
    {
        Slime.Instance.SetElementForced(ElementType.Fire);
    }
    public void SetWater()
    {
        Slime.Instance.SetElementForced(ElementType.Water);
    }
    public void SetAir()
    {
        Slime.Instance.SetElementForced(ElementType.Air);
    }
    public void SetIce()
    {
        Slime.Instance.SetElementForced(ElementType.Ice);
    }
    public void SetElectro()
    {
        Slime.Instance.SetElementForced(ElementType.Electricity);
    }
    public void SetEmpty()
    {
        Slime.Instance.SetElementForced(ElementType.None);
    }
}
