using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bike : MonoBehaviour
{
    private IWaiter _waiter;
    [SerializeField] private float _timeCounter;
    [SerializeField] private float _flagCounter;
    
    
    [SerializeField] private List<BikeOrder> _bikeOrders;
    [SerializeField] private MoneyArea _moneyArea;
  
    public int orderCount;
    public int completeCount;

    
    private void OnTriggerEnter(Collider other)
    {
        IWaiter iWaiter = other.GetComponent<IWaiter>();
        if (iWaiter!=null)
        {
            _waiter = iWaiter;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        IWaiter iWaiter = other.GetComponent<IWaiter>();
        if (iWaiter!=null)
        {
            _waiter = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (TimeCount() && _waiter.IsHaveFood() && orderCount>completeCount)
        {
            for (int j = 0; j < _bikeOrders.Count; j++)
            {
                BikeOrder bikeOrder = _bikeOrders[j];
                for (int k = 0; k <bikeOrder.order.Count; k++)
                {
                    if (bikeOrder.order[k].foodType ==_waiter.GetFood().foodType)
                    {
                        Food food = _waiter.FoodService();
                        bikeOrder.CompleteOrder(k);
                        food.transform.SetParent(null);
                        food.transform.DOJump(OrderManager.Instance.bike.transform.position, 1, 1, 0.2f)
                            .OnComplete((() =>
                            {
                                Destroy(food.gameObject);
                            }));
                        return;
                    }
                }
            }
        }
           
    }
    
    private bool TimeCount()
    {
        if (_waiter == null) return false;
        _timeCounter += Time.deltaTime;
        if (_timeCounter < _flagCounter) return false;
        _timeCounter = 0;
        return true;
    }

    public void AddOrder(BikeOrder bikeOrder)
    {
        _bikeOrders.Add(bikeOrder);
    }

    public void ClearOrder()
    {
      OrderCanvas.Instance.bikePanel.gameObject.SetActive(true);
        orderCount = 0;
        completeCount = 0;
        foreach (BikeOrder bikeOrder in _bikeOrders)
        {
            bikeOrder.ClearOrder();
        }
        _bikeOrders.Clear();
     
    }

    public void PayMoney()
    {
        for (int i = 0; i < orderCount; i++)
        {
            _moneyArea.AddMoney();
        }
        ClearOrder();
    }

    public void GoService()
    {
        OrderCanvas.Instance.bikePanel.gameObject.SetActive(false);
        transform.DOLocalMove(transform.localPosition + Vector3.forward*20,4).SetSpeedBased()
            .OnComplete((() =>
            {
                StartCoroutine(ReturnRestaurant());
            }));
    }

    public void OrderCompleteControl()
    {
        completeCount++;
        if (completeCount==orderCount)
        {
            OrderCanvas.Instance.bikePanel.gameObject.SetActive(false);
            OrderCanvas.Instance.restaurantPanel.gameObject.SetActive(true);
            GoService();
        }
    }

    public IEnumerator ReturnRestaurant()
    {
        transform.localEulerAngles = new Vector3(0, 180, 0);
        yield return new WaitForSeconds(10f);
        transform.DOLocalMove(Vector3.zero, 5f).SetSpeedBased()
            .OnComplete((() =>
            {
                transform.localEulerAngles=Vector3.zero;
                PayMoney();
            }));
    }
}
