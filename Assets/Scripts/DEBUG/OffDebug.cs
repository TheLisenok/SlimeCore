using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffDebug : MonoBehaviour
{
    [SerializeField] private List<GameObject> whatOff = new List<GameObject>();
    void Start()
    {
        foreach(GameObject o in whatOff)
        {
            o.SetActive(false);
        }
    }

}
