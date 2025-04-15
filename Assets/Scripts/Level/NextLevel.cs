using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : Interactable
{
    public override void OnInteract()
    {
        base.OnInteract();

        GameManager.Instance.NextLevel();
    }
}
