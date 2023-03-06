using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MoneyArea : MonoBehaviour
{
    [SerializeField] private Transform _stackPoint;
    private float _timeCounter;
    public float _timeFlag;
    private Camera _cam;
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void OnTriggerStay(Collider other)
    {
      
        if (other.CompareTag("Player") && _stackPoint.childCount>0)
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter>=_timeFlag)
            {
                _audioSource.Play();
                _timeCounter = 0;
               SendMoney(_stackPoint.GetChild(_stackPoint.childCount-1));
            }
        }
    }


    private void SendMoney(Transform money)
    {
        money.SetParent(null);
        OrderManager.Instance.SetMoney(10);
        Transform moneyUI = CanvasManager.Instance.InstantiateMoneyUI();
        moneyUI.position = _cam.WorldToScreenPoint(money.position);
        moneyUI.DOJump(CanvasManager.Instance.moneyUI.position, 1,1,0.3f)
            .OnComplete((() => Destroy(moneyUI.gameObject)));
        Destroy(money.gameObject);
        
    }

    public void AddMoney()
    {
        Instantiate(_moneyPrefab, _stackPoint);
    }
}
