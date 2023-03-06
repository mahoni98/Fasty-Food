using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class SellZone : MonoBehaviour
{
    [SerializeField] private Car _car;
    private IWaiter _waiter;
    [SerializeField] private float _timeCounter;
    [SerializeField] private float _flagCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            _car = other.GetComponent<Car>();
            _timeCounter = 0;
        }

        IWaiter waiter = other.GetComponent<IWaiter>();
        if (waiter!=null)
        {
            _waiter = waiter;
            _timeCounter = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (TimeCount() && _waiter.IsHaveFood() && _car.order.Count>0)
        {
            for (int i = 0; i < _car.order.Count; i++)
            {
                if (_car.order[i].foodType==_waiter.GetFood().foodType)
                {
                    Food food = _waiter.FoodService();
                    food.transform.DOJump(_car.transform.position, 1, 1, 0.4f)
                        .OnComplete((() =>
                        {
                            Destroy(food.gameObject);
                            _car.ClearOrder(i);
                        }));
                    break;
                }
            }
        }
           
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            _car = null;
            _timeCounter = 0;
        }
        
        IWaiter waiter = other.GetComponent<IWaiter>();
        if (waiter!=null)
        {
            _waiter = null;
            _timeCounter = 0;
        }
    }
    

    private bool TimeCount()
    {
        if (_car == null || _waiter == null) return false;
        _timeCounter += Time.deltaTime;
        if (_timeCounter < _flagCounter) return false;
        _timeCounter = 0;
        return true;
    }
}
