using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    public ScriptableObjectInt money;
    [SerializeField] private TextMeshProUGUI moneyText;


    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneyText.text = money.value.ToString();
    }

}
