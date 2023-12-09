using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[SelectionBase]
public class CookBench : MonoBehaviour
{
    [SerializeField] private float counter;
    [SerializeField] private Image timeBar;

    [SerializeField] private UpgradeObject _level;
    [SerializeField] private UpgradeObject _speed;

    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;
    [SerializeField] private GameObject figure;
    [SerializeField] private GameObject player;
    [SerializeField] private float maxDistanceToCollect;

    private int foodIndex1;
    private int foodIndex2;
    private int foodIndex3;

    private GameObject food1;
    private GameObject food2;
    private GameObject food3;

    private bool isSpawned1;
    private bool isSpawned2;
    private bool isSpawned3;

    private float foodSpawnTime;
    private CarryPlates carry;

    void Start()
    {
        carry = player.GetComponent<CarryPlates>();

        figure.GetComponent<Animator>().SetInteger("Condition", 2);
        SetSpawnTime();
    }

    public void SetSpawnTime()
    {
        foodSpawnTime = 3 + (10 - (_speed.upgradeLevel * 2f));
    }

    private void FixedUpdate()
    {
        if (carry.maxPlateAmount > carry.plates.Count) {
            if (isSpawned1)
            {
                if (Vector3.Distance(spawnPoint1.position, player.transform.position) < maxDistanceToCollect)
                {
                    food1.SetActive(false);
                    carry.AddFood(foodIndex1);
                    isSpawned1 = false;
                }
            }

            if (isSpawned2)
            {
                if (Vector3.Distance(spawnPoint2.position, player.transform.position) < maxDistanceToCollect)
                {
                    food2.SetActive(false);
                    carry.AddFood(foodIndex2);
                    isSpawned2 = false;
                }
            }

            if (isSpawned3)
            {
                if (Vector3.Distance(spawnPoint3.position, player.transform.position) < maxDistanceToCollect)
                {
                    food3.SetActive(false);
                    carry.AddFood(foodIndex3);
                    isSpawned3 = false;
                }
            }
        }
    }

    void Update()
    {
        if (!isSpawned1 || !isSpawned2 || !isSpawned3)
        {
            counter += 1 * Time.deltaTime;
            if (counter > foodSpawnTime)
            {
                List<int> foodsSameLevel = new List<int>();
                for (int i = 0; i < ObjectPool.instance.foodsByLevelList.Count; i++)
                {
                    if (_level.upgradeLevel - 1 == ObjectPool.instance.foodsByLevelList[i].food.foodLevel)
                        foodsSameLevel.Add(i);
                }
                int randomValue = UnityEngine.Random.Range(0, foodsSameLevel.Count);

                if (!isSpawned1)
                {
                    foodIndex1 = foodsSameLevel[randomValue];
                    food1 = ObjectPool.instance.GetPooledObject(foodIndex1);
                    food1.transform.position = spawnPoint1.position;
                    isSpawned1 = true;
                }
                else if (!isSpawned2)
                {
                    foodIndex2 = foodsSameLevel[randomValue];
                    food2 = ObjectPool.instance.GetPooledObject(foodIndex2);
                    food2.transform.position = spawnPoint2.position;
                    isSpawned2 = true;
                }
                else if (!isSpawned3)
                {
                    foodIndex3 = foodsSameLevel[randomValue];
                    food3 = ObjectPool.instance.GetPooledObject(foodIndex3);
                    food3.transform.position = spawnPoint3.position;
                    isSpawned3 = true;
                }
                counter = 0;
            }
            timeBar.fillAmount = counter / foodSpawnTime;
        }
    }
}

