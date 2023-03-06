using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Car _car;
    public Animator animator;
    [SerializeField] private float _timeCounter;
    [SerializeField] private float _flagCounter;
    public List<ServiceTable> serviceTables;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            _car = other.GetComponent<Car>();
            _timeCounter = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (TimeCount() && _car.order.Count>0)
        {
            for (int i=0;i<_car.order.Count;i++)
            {
                foreach (ServiceTable serviceTable in serviceTables)
                {
                    if (serviceTable.foodType==_car.order[i].foodType && serviceTable._stackPoint.childCount>0)
                    {
                        _timeCounter = 0;
                        Transform food = serviceTable._stackPoint.GetChild(serviceTable._stackPoint.childCount - 1);
                        food.SetParent(null);
                        _car.ClearOrder(i);
                        DOTween.Kill(food);
                    food.DOJump(_car.transform.position, 1, 1, 0.5f)
                            .OnComplete((() =>
                            {
                                Destroy(food.gameObject);
                            }));
                  
                    animator.SetTrigger("Service");
                    return;
                    }
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
    }
    
    private bool TimeCount()
    {
        if (_car == null) return false;
        _timeCounter += Time.deltaTime;
        if (_timeCounter < _flagCounter) return false;

        _timeCounter = 0;
        return true;
    }
}
