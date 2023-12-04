using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Action action;

    void Start()
    {
        action += bir;
        action += iki;
        action?.Invoke();
    }

    void Update()
    {
        
    }


    void bir()
    {
        Debug.Log("1");
    }
    void iki()
    {
        Debug.Log("2");
    }
}
