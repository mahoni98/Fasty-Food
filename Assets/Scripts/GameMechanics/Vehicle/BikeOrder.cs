using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BikeOrder : MonoBehaviour
{
    
    private IWaiter _waiter;
    [SerializeField] private float _timeCounter;
    [SerializeField] private float _flagCounter;
    
    [SerializeField] private AreaType _areaType;
    private OrderBox orderBox;
    public List<Food> order;
    private int cleanedOrderCount;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    void Start()
    { orderBox = OrderCanvas.Instance.SetOrder(_areaType);
        OrderManager.Instance.bike.AddOrder(this);
      SetOrder();
     
    }

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
        if (TimeCount() && _waiter.IsHaveFood() && order.Count>0)
        {
            for (int i = 0; i < order.Count; i++)
                {
                    if (order[i].foodType==_waiter.GetFood().foodType)
                    {
                        Food food = _waiter.FoodService();
                        CompleteOrder(i);
                        orderBox.transform.GetChild(i+cleanedOrderCount).GetChild(0).gameObject.SetActive(true);
                        cleanedOrderCount++;
                        order.Remove(order[i]);
                        OrderManager.Instance.bike.OrderCompleteControl();
                        if (order.Count==0)
                        {
                            _collider.enabled = false;
                            // orderBox.CompletedOrder(_areaType);
                            // isOrderCompleted = true;
                            // Destroy(gameObject);
                            // OrderManager.Instance.bikeOrders.Remove(gameObject);
                        }
                        food.transform.DOJump(OrderManager.Instance.bike.transform.position, 1, 1, 0.4f)
                            .OnComplete((() =>
                            {
                                Destroy(food.gameObject);
                            }));
                    return;
                    }
                }
            
          
        }
    }

    

    void SetOrder()
    {
        int orderCount = Random.Range(1, 4);
        OrderManager.Instance.bike.orderCount += orderCount;
        List<Food> foods;
        if (_areaType==AreaType.Cafe)
        {
            foods = OrderManager.Instance._cafeMaterials;
        }
        else
        {
            foods = OrderManager.Instance._restaurantsMaterials;
        }
        for (int i = 0; i < orderCount; i++)
        {
            
            order.Add(foods[Random.Range(0,foods.Count)]);
         Image image = Instantiate(OrderCanvas.Instance.orderImage,orderBox.transform);
         image.sprite = order[i].sprite;
         image.transform.SetSiblingIndex(i);
         orderBox._Images.Add(image);
        }
    }
    
    public void CompleteOrder(int count)
    {

        for (int i = 0; i < orderBox._Images.Count; i++)
        {
            if (!orderBox._Images[i].transform.GetChild(0).gameObject.activeSelf && orderBox._Images[i].sprite==order[count].sprite)
            {
                orderBox._Images[i].transform.GetChild(0).gameObject.SetActive(true);
                break;
            }
        }
     
        cleanedOrderCount++;
        order.Remove(order[count]);
        OrderManager.Instance.bike.OrderCompleteControl();
        if (order.Count==0)
        {
            _collider.enabled = false;
            // orderBox.CompletedOrder(_areaType);
            // isOrderCompleted = true;
            // Destroy(gameObject);
            // OrderManager.Instance.bikeOrders.Remove(gameObject);
        }
    }

    public void ClearOrder()
    {
        OrderManager.Instance.bikeOrders.Remove(gameObject);
        Destroy(orderBox.gameObject);
        Destroy(gameObject);
    }

    private bool TimeCount()
    {
        if (_waiter == null) return false;
        _timeCounter += Time.deltaTime;
        if (_timeCounter < _flagCounter) return false;
        _timeCounter = 0;
        return true;
    }
}
