using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionEffect : MonoBehaviour
{
    #region Singletone
    public static ReactionEffect Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    #endregion

    // TODO: ������� ��� ������� ������� � �������� �� ����
    [SerializeField] private GameObject windZone;
    [SerializeField] private GameObject fireWhirlwind;
    [SerializeField] private GameObject caboom;

    public void SetWindZone()
    {
        Vector3 spawnPos = Slime.Instance.gameObject.transform.position; // ���� ������� ������ ��� ������ ��������� ������
        //spawnPos = spawnPos.normalized;

        Instantiate(windZone, spawnPos, Quaternion.identity);

    }

    public void SetFireWhirlwind()
    {
        Vector3 spawnPos = Slime.Instance.gameObject.transform.position; // ���� ������� ������ ��� ������ ��������� ������

        Instantiate(fireWhirlwind, spawnPos, Quaternion.identity);
    }

    public void SetCaboom()
    {
        Vector3 spawnPos = Slime.Instance.gameObject.transform.position; // ���� ������� ������ ��� ������ ��������� ������

        Instantiate(caboom, spawnPos, Quaternion.identity);
    }
}
