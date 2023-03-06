using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{

    private TextMeshProUGUI _moneyText;
    private void Awake()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        OrderManager.Instance.OnMoneyChange += ChangeText;
        ChangeText(OrderManager.Instance.Money);
    }

    // Update is called once per frame
    void ChangeText(int money)
    {
        _moneyText.text = money.ToString();
    }
}
