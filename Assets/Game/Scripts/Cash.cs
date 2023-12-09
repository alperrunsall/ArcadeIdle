using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cash : MonoBehaviour
{
    public static Cash instance;
    public CashBench cashBench;

    void Awake()
    {
        if(instance == null)
            instance = this;

        /**/
        //Accumulate();
    }

    public void Accumulate()
    {
        transform.parent = cashBench.accumulatePlace;
        transform.DOLocalMove(CalculatePosition(cashBench.accumulatePlace),
            1f).SetEase(Ease.OutCubic);
        cashBench.accumulateList.Add(transform);

        /*transform.DOLocalMove(new Vector3(CalculatePosition(cashBench.accumulatePlace).x, 7f, CalculatePosition(cashBench.accumulatePlace).z),
            0.4f)
            .SetEase(Ease.Linear)
            .OnComplete(() => {

                transform.DOLocalMove(new Vector3(CalculatePosition(cashBench.accumulatePlace).x, 0f, CalculatePosition(cashBench.accumulatePlace).z),
                    0.2f)
                    //Vector3.Distance(transform.position, target.position) / 15f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => {
                    });
            });*/
    }

    private Vector3 CalculatePosition(Transform origin)
    {
        int listCount = cashBench.accumulateList.Count;
        int xPos = listCount % 4;
        int yPos = listCount / 12;
        int zPos = ((listCount - (yPos * 12)) / -4) % 4;

        return new Vector3(xPos, yPos / 2f, zPos * 1.5f);
    }
    public void Collect(Transform target)
    {
        //transform.parent = target;
        transform.DOMove(new Vector3(target.position.x, 7f, target.position.z),
            Vector3.Distance(transform.position, target.position) / 8f)
            .SetEase(Ease.Linear)
            .OnComplete(() => {

                transform.DOMove(new Vector3(target.position.x, 0f, target.position.z),
                    0.2f)
                    //Vector3.Distance(transform.position, target.position) / 15f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => {
                        Settings.instance.money.value += 10;
                        Settings.instance.UpdateMoney();
                        cashBench.accumulateList.Remove(transform);
                        gameObject.SetActive(false);
                    });
            });
    }
}
