using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    [SerializeField] private Transform _carModels;
    [SerializeField] private bool isOrderCompleted;
    [SerializeField] private PathFollower _pathFollower;
    [SerializeField] private AreaType _areaType;
    [SerializeField] private AudioSource _audio;
    private OrderBox orderBox;
    private float _speed;
    public List<Food> order;
    public bool isStaySellArea;
    private Animator _animator;
    private int cleanedOrderCount;
    private void Awake()
    {
        int carModel= Random.Range(0,_carModels.childCount);
        _carModels.GetChild(carModel).gameObject.SetActive(true);
        _animator = _carModels.GetChild(carModel).GetChild(0).GetComponent<Animator>();
        _pathFollower = GetComponent<PathFollower>();
        _speed = _pathFollower.speed;
    
    }

    void Start()
    {
        orderBox = OrderCanvas.Instance.SetOrder(_areaType);
      SetOrder();
      InvokeRepeating(nameof(ControlDistance),0,0.2f);
    }

    private void ControlDistance()
    {
        
        if (isStaySellArea) return;
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit,10))
        {
            if (hit.collider.CompareTag("Car"))
            {
                _pathFollower.speed = 0;
                return;
            }

            _pathFollower.speed = _speed;

        }
        else
        {
            _pathFollower.speed = _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ServicePoint"))
        {
            StartCoroutine(WaitFood());
        }
    }

    IEnumerator WaitFood()
    {
        yield return new WaitForSeconds(8f);
        _audio.Play();
    }


    private void OnTriggerStay(Collider other)
    {
        if (isOrderCompleted)
        {
            _pathFollower.speed = _speed;
            return;
        }
        if (other.CompareTag("ServicePoint"))
        {
            _animator.SetInteger("Anim",2);
            isStaySellArea = true;
            _pathFollower.speed = 0;
        }
    }

    void SetOrder()
    {
        int orderCount = Random.Range(1, 4);
        List<Food> foods;
        if (_areaType==AreaType.Restaurant)
        {
            foods = OrderManager.Instance._restaurantsMaterials;
        }
        else
        {
            foods = OrderManager.Instance._cafeMaterials;
        }
        for (int i = 0; i < orderCount; i++)
        {
         order.Add(foods[Random.Range(0,foods.Count)]);
         Image image = Instantiate(OrderCanvas.Instance.orderImage,orderBox.transform);
         image.sprite = order[i].sprite;
         image.transform.SetSiblingIndex(i);
        }
    }
    
    public void ClearOrder(int  count)
    {
  
        orderBox.transform.GetChild(count+cleanedOrderCount).GetChild(0).gameObject.SetActive(true);
        cleanedOrderCount++;
        OrderManager.Instance.PayMoney(_areaType);
        order.Remove(order[count]);
        if (order.Count==0)
        {
            StopCoroutine(WaitFood());
            _animator.SetInteger("Anim",1);
            orderBox.CompletedOrder(_areaType);
            Invoke(nameof(Go),0.3f);
            Destroy(gameObject,4f);
            if (_areaType==AreaType.Cafe)
            {
                OrderManager.Instance.cafeOrders.Remove(gameObject);
                return;
            }
            OrderManager.Instance.restaurantOrders.Remove(gameObject);
        

        }
    }

    public void Go()
    {
        isOrderCompleted = true;
    }
}
