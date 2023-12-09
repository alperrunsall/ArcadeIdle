using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject player;
    [SerializeField] private ScriptableObjectInt money;
    [SerializeField] private UpgradeObject _object;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image background;

    public bool chair;
    public DinnerTable dinnerTable;

    private float timer = 3f;
    private bool inside = false;
    private bool locked = false;

    private void Start()
    {
        SetText();
        if (_object.upgradeLevel == _object.maxLevel)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (inside && !locked)
            CountDownTimer();
    }

    private void CountDownTimer()
    {
        if (timer < 0)
        {
            int count = _object.GetCost() / 10 <= 20 ? _object.GetCost() / 10 : _object.GetCost() / 20;
            for (int i = 0; i < count; i++)
            {
                GameObject cash = ObjectPool.instance.GetPooledCash();
                cash.transform.position = new Vector3(player.transform.position.x, 7f ,player.transform.position.z);
                cash.transform.DOMove(transform.position, (float) i * 0.04f).OnComplete(() => {
                    cash.SetActive(false);
                });
            }

            money.value -= _object.GetCost();
            _object.upgradeLevel += 1;

            locked = true;
            timer = 3;

            SetText();
            Settings.instance.UpdateMoney();

            if (parent.TryGetComponent<CookBench>(out CookBench bench))
            {
                bench.SetSpawnTime();
            }

            if (chair)
            {
                Map.instance.chairs.Add(dinnerTable);
                dinnerTable.gameObject.SetActive(true);
                PlayerPrefs.SetInt("chair_count", PlayerPrefs.GetInt("chair_count") + 1);
                Map.instance.Check();
                gameObject.SetActive(false);
            }
            if (_object.upgradeLevel == _object.maxLevel)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            timer -= 1 * Time.deltaTime;
            text.text = timer.ToString("0");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            if (money.value >= _object.GetCost()) {
                inside = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            inside = false;
            locked = false;
            timer = 3f;
            SetText();
        }
    }

    private void SetText()
    {
        text.text = "(" + _object.upgradeLevel + ")\n+" 
            + _object.name + "\n-" 
            + _object.GetCost();
    }

}
