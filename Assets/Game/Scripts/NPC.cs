using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class NPC : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image timeBar;

    public int targetType = 0; // 0 = waiting area, 1 = chair, 2 = exit chair, 3 = pay, 4 = exit
    public int whichChair;

    public Vector3 target;
    public DinnerTable table;

    private int foodIndex;
    private Animator animator;
    private Coroutine eatingCor;
    private Coroutine payingCor;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        if (targetType == 3)
        {
            if (target == Vector3.zero) {
                target = new Vector3(Map.instance.pay.transform.position.x - ((Map.instance.pay.queue.Count - 1) * 3f),
                    0.3f, Map.instance.pay.transform.position.z);
            }
        }
        if (targetType == 5)
            target = Map.instance.exit.transform.position;

        if (target != Vector3.zero)
        {
            transform.LookAt(target);

            if (Vector3.Distance(transform.position, target) < maxDistance)
            {
                if(targetType == 0)
                    animator.SetInteger("State", 0);
                else if(targetType == 1)
                {
                    if(whichChair == 1)
                    {
                        if (table.food1Index == -1)
                            animator.SetInteger("State", 2);
                        else
                        {
                            animator.SetInteger("State", 3);
                            if (eatingCor == null)
                            {
                                canvas.SetActive(true);
                                eatingCor = StartCoroutine(Eating());
                            }
                        }
                    }
                    if (whichChair == 2)
                    {
                        if (table.food2Index == -1)
                            animator.SetInteger("State", 2);
                        else
                        {
                            animator.SetInteger("State", 3);
                            if (eatingCor == null)
                            {
                                canvas.SetActive(true);
                                eatingCor = StartCoroutine(Eating());
                            }
                        }
                    }

                    transform.position = target;

                    float rotationY = whichChair == 1 ? 270f : 90f; 
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
                }
                else if (targetType == 2)
                {
                    Map.instance.pay.queue.Add(transform);
                    target = Vector3.zero;
                    targetType = 3;
                }
                else if (targetType == 3)
                {
                    animator.SetInteger("State", 0);
                    target = new Vector3(Map.instance.pay.transform.position.x - ((Map.instance.pay.queue.IndexOf(transform) * 3f)),
                       0.3f, Map.instance.pay.transform.position.z);
                    if (Vector3.Distance(transform.position, Map.instance.pay.transform.position) < maxDistance+1f) {
                        if (whichChair == 1)
                        {
                            for (int i = 0; i < ObjectPool.instance.foodsByLevelList[foodIndex].food.foodFee / 10; i++)
                            {
                                Cash cash = ObjectPool.instance.GetPooledCash().GetComponent<Cash>();
                                cash.transform.position = transform.position;
                                cash.cashBench = Map.instance.pay;
                                cash.Accumulate();
                            }

                        }
                        if (whichChair == 2)
                        {
                            for (int i = 0; i < ObjectPool.instance.foodsByLevelList[foodIndex].food.foodFee / 10; i++)
                            {
                                Cash cash = ObjectPool.instance.GetPooledCash().GetComponent<Cash>();
                                cash.transform.position = transform.position;
                                cash.cashBench = Map.instance.pay;
                                cash.Accumulate();
                            }
                        }
                        targetType = 4;
                    }
                }
                else if (targetType == 4)
                {
                    if (payingCor == null)
                        payingCor = StartCoroutine(Pay());
                }
                else if (targetType == 5)
                {
                    transform.position = new Vector3(-60, 0, 0);
                    Map.instance.customerQue.Add(this);
                    Map.instance.Check();
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
                animator.SetInteger("State", 1);
            }

        }
    }


    IEnumerator Pay()
    {
        yield return new WaitForSeconds(2);
        targetType = 5;
        Map.instance.pay.queue.Remove(transform);
        StopCoroutine(payingCor);
        payingCor = null;

    }
    IEnumerator Eating()
    {
        float elapsedTime = 0f;
        float waitingTime = 5f;

        while (elapsedTime < waitingTime)
        {
            canvas.transform.LookAt(Map.instance.mainCam);
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / waitingTime;
            timeBar.fillAmount = progress;
            yield return null;
        }

        if (whichChair == 1)
        {
            foodIndex = table.food1Index;
            table.food1Index = -1;
            table.place1Full = false;
            table.food1.SetActive(false);
        }
        if (whichChair == 2)
        {
            foodIndex = table.food2Index;
            table.food2Index = -1;
            table.place2Full = false;
            table.food2.SetActive(false);
        }

        target = table.exit.position;
        targetType = 2;
        Map.instance.Check();
        canvas.SetActive(false);
        StopCoroutine(eatingCor);
        eatingCor = null;
    }
}
