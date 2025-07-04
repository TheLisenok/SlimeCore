using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : BreakableObject
{
    [SerializeField] private float burnDuration = 3f;
    [SerializeField] private ParticleSystem fireParticles;

    private bool isBurning = false;

    public void Ignite()
    {
        if (isBurning) return;

        isBurning = true;
        currentElement = ElementManager.Instance.CreateElementFromType(ElementType.Fire);
        fireParticles?.Play();
        Invoke(nameof(BurnOut), burnDuration);
    }

    private void BurnOut()
    {
        Break();
    }

    public void Extinguish()
    {
        if (!isBurning) return;

        isBurning = false;
        fireParticles?.Stop();
        CancelInvoke(nameof(BurnOut));
    }
}
