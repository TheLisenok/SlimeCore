using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private bool isFireResist = false;
    [SerializeField] private float burnTime = 3f;

    [SerializeField] private ParticleSystem burnParticle;

    public void Burn()
    {
        if (!isFireResist)
        {
            // ��������� ���������� ������ �������
            burnParticle.Play();

            Destroy(gameObject, burnTime);
        }
    }

    public void Break()
    {
        Destroy(gameObject);
    }
}
