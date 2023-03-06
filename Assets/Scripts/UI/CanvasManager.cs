using System.Collections;
using System.Collections.Generic;
using Managers.Singleton;
using UnityEngine;

public class CanvasManager : SingletonManager<CanvasManager>
{
    public Canvas canvas;
    public GameObject moneyUIPrefab;
    public Transform moneyUI;


    protected override void Awake()
    {
        base.Awake();

        canvas = GetComponent<Canvas>();
    }
    void Start()
    {
        
    }


    public Transform InstantiateMoneyUI()
    {

        GameObject money = Instantiate(moneyUIPrefab, transform);
        return money.transform;
    }

}
