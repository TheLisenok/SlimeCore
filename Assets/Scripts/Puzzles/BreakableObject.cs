using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : ElementHolder
{
    [SerializeField] protected float destroyDelay = 0f;

    public virtual void Break()
    {
        Destroy(gameObject, destroyDelay);
    }
}
