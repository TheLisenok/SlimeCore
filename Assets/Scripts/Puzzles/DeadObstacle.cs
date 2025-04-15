using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadObstacle : Interactable
{
    [SerializeField] private int damage = 1;
    public override void OnInteract()
    {
        base.OnInteract();

        Slime.Instance.TakeDamage(damage);
    }
}
