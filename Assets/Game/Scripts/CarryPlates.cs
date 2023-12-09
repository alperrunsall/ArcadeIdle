using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryPlates : MonoBehaviour
{
    [SerializeField] private Transform platesTransform;
    public int maxPlateAmount = 5;
    public List<GameObject> plates = new List<GameObject>();

    public void AddFood(int foodIndex)
    {
        GameObject parent = new GameObject();
        parent.name = "Platter(Parent)";
        Plate plateComp = parent.AddComponent<Plate>();

        GameObject plate = ObjectPool.instance.GetPooledPlate();
        Vector3 platePos = calculateVector();
        plate.transform.position = platePos;
        plate.transform.parent = parent.transform;
        plateComp.plate = plate;

        GameObject food = ObjectPool.instance.GetPooledObject(foodIndex);
        food.transform.position = platePos;
        food.transform.parent = parent.transform;
        plateComp.food = food;

        plateComp.index = foodIndex;
        plateComp.size = food.GetComponent<Renderer>().bounds.size.y;
        parent.transform.parent = platesTransform;
        plates.Add(parent);
    }

    private Vector3 calculateVector()
    {
        float totalSize = 0;

        if (plates.Count == 0)
            return platesTransform.position;

        for (int i = 0; i < plates.Count; i++)
            totalSize += plates[i].GetComponent<Plate>().size;

        return new Vector3(platesTransform.position.x, platesTransform.position.y + totalSize, platesTransform.position.z);
    }
}
