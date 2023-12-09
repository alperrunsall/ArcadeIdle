using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashBench : MonoBehaviour
{
    public static CashBench instance;
    public Transform player;
    public List<Transform> accumulateList = new List<Transform>();
    public List<Transform> queue = new List<Transform>();
    public Transform accumulatePlace;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            foreach (Transform cash in accumulateList)
                cash.GetComponent<Cash>().Collect(player);
    }
}
