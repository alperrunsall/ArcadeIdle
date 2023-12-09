using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class DinnerTable : MonoBehaviour
{
    public Transform exit;
    public Transform sitPlace1;
    public Transform sitPlace2;
    public Transform foodPlace1;
    public Transform foodPlace2;
    public GameObject food1;
    public GameObject food2;
    public int food1Index;
    public int food2Index;
    public bool place1Full;
    public bool place2Full;
}
