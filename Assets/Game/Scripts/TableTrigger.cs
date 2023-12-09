using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTrigger : MonoBehaviour
{
    [SerializeField] private DinnerTable table;
    [SerializeField] private int whichChair;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CarryPlates>(out CarryPlates list))
        {
            if (list.plates.Count > 0) {

                if (whichChair == 1)
                {
                    if (table.food1Index == -1)
                    {
                        int index = list.plates[0].GetComponent<Plate>().index;
                        GameObject food = ObjectPool.instance.GetPooledObject(index);
                        food.transform.position = table.foodPlace1.position;
                        table.food1Index = index;
                        table.food1 = food;
                        Init(list);
                    }
                }
                else if (whichChair == 2)
                {
                    if (table.food2Index == -1)
                    {
                        int index = list.plates[0].GetComponent<Plate>().index;
                        GameObject food = ObjectPool.instance.GetPooledObject(index);
                        food.transform.position = table.foodPlace2.position;
                        table.food2Index = index;
                        table.food2 = food;
                        Init(list);
                    }
                }
            }
        }
    }

    private void Init(CarryPlates list)
    {
        list.plates[0].GetComponent<Plate>().food.transform.parent = null;
        list.plates[0].GetComponent<Plate>().food.SetActive(false);
        list.plates[0].GetComponent<Plate>().plate.transform.parent = null;
        list.plates[0].GetComponent<Plate>().plate.SetActive(false);
        list.plates[0].SetActive(false);
        list.plates.RemoveAt(0);
    }
}
