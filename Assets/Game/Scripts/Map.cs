using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map instance;
    public Transform exit;
    public Transform mainCam;
    public CashBench pay;
    public int chairCount;
    public List<DinnerTable> chairs = new List<DinnerTable>();
    public List<GameObject> chairCanvas = new List<GameObject>();

    public Transform waitingArea;
    public List<NPC> customerQue = new List<NPC>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        if (PlayerPrefs.GetInt("chair_count") == 0)
        {
            PlayerPrefs.SetInt("chair_count", 1);
            chairCount = 1;
        }
    }

    public void Check()
    {
        chairCount = PlayerPrefs.GetInt("chair_count");
        for (int i = 0; i < chairCount; i++)
        {
            chairs[i].gameObject.SetActive(true);

            if (!chairs[i].place1Full)
            {
                customerQue[0].target = chairs[i].sitPlace1.position;
                customerQue[0].targetType = 1;
                customerQue[0].whichChair = 1;
                customerQue[0].table = chairs[i];
                chairs[i].place1Full = true;
                customerQue.RemoveAt(0);
            }
            if (!chairs[i].place2Full)
            {
                customerQue[0].target = chairs[i].sitPlace2.position;
                customerQue[0].targetType = 1;
                customerQue[0].whichChair = 2;
                customerQue[0].table = chairs[i];
                chairs[i].place2Full = true;
                customerQue.RemoveAt(0);
            }

            if (i > 0)
                chairCanvas[i - 1].SetActive(false);
        }
        Queue();
    }

    public void Queue()
    {
        for (int i = 0; i < customerQue.Count; i++)
        {
            customerQue[i].target = new Vector3(waitingArea.position.x - (i * 3), 0.3f, waitingArea.position.z);
            customerQue[i].targetType = 0;
        }
    }
}
