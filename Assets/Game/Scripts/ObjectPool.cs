using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<FoodLevel> foodsByLevelList;

    [SerializeField] int cashAmount;
    [SerializeField] GameObject cashPrefab;
    public List<GameObject> cashPool;

    [SerializeField] int plateAmount;
    [SerializeField] GameObject platePrefab;
    public List<GameObject> platePool;

    [SerializeField] int customerAmount;
    [SerializeField] GameObject customerPrefab;
    public List<GameObject> customerPool;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    void Start()
    {
        for (int i = 0; i < foodsByLevelList.Count; i++)
        {
            GameObject parent = null;
            while (foodsByLevelList[i].pooledList.Count < 15)
            {
                GameObject obj = Instantiate(foodsByLevelList[i].food.foodModel);
                obj.SetActive(false);

                if (parent == null)
                {
                    parent = new GameObject();
                    parent.name = foodsByLevelList[i].food.foodName + "(Pool)";
                }

                obj.transform.parent = parent.transform;
                obj.GetComponent<Food>().parent = parent.transform;
                foodsByLevelList[i].pooledList.Add(obj);
            }
            parent = null;
        }

        GameObject cashParent = null;
        for (int i = 0; i < cashAmount; i++)
        {
            if (cashParent == null)
            {
                cashParent = new GameObject();
                cashParent.name = "Cash(Pool)";
            }
            GameObject obj = Instantiate(cashPrefab);
            obj.SetActive(false);
            obj.transform.parent = cashParent.transform;
            cashPool.Add(obj);
        }

        GameObject plateParent = null;
        for (int i = 0; i < plateAmount; i++)
        {
            if (plateParent == null)
            {
                plateParent = new GameObject();
                plateParent.name = "Plate(Pool)";
            }
            GameObject obj = Instantiate(platePrefab);
            obj.SetActive(false);
            obj.transform.parent = plateParent.transform;
            platePool.Add(obj);
        }

        GameObject customerParent = null;
        for (int i = 0; i < customerAmount; i++)
        {
            if (customerParent == null)
            {
                customerParent = new GameObject();
                customerParent.name = "Customer(Pool)";
            }
            GameObject obj = Instantiate(customerPrefab);
            obj.transform.position = new Vector3(-60,0,0);
            obj.transform.parent = customerParent.transform;
            customerPool.Add(obj);
            Map.instance.customerQue.Add(obj.GetComponent<NPC>());
        }
        Map.instance.Check();
    }
    public GameObject GetPooledCustomer()
    {
        for (int i = 0; i < customerPool.Count; i++)
        {
            if (!customerPool[i].activeInHierarchy)
            {
                customerPool[i].SetActive(true);
                return customerPool[i];
            }
        }

        return null;
    }

    public GameObject GetPooledPlate()
    {
        for (int i = 0; i < platePool.Count; i++)
        {
            if (!platePool[i].activeInHierarchy)
            {
                platePool[i].SetActive(true);
                return platePool[i];
            }
        }

        return null;
    }

    public GameObject GetPooledCash()
    {
        for (int i = 0; i < cashPool.Count; i++)
        {
            if (!cashPool[i].activeInHierarchy)
            {
                cashPool[i].SetActive(true);
                return cashPool[i];
            }
        }

        return null;
    }

    public GameObject GetPooledObject(int index)
    {
        for (int i = 0; i < foodsByLevelList[index].pooledList.Count; i++)
        {
            if (!foodsByLevelList[index].pooledList[i].activeInHierarchy)
            {
                foodsByLevelList[index].pooledList[i].SetActive(true);
                return foodsByLevelList[index].pooledList[i];
            }
        }

        return null;
    }

}

[Serializable]
public struct FoodLevel
{
    public Foods food;
    public List<GameObject> pooledList;
}