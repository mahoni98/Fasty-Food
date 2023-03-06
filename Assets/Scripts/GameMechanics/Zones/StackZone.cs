using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StackZone : MonoBehaviour
{
    public List<Food> _foods;
    [SerializeField] private Image _image;
    private float _timeCounter;
    private IWaiter _waiter;
    

    private void OnTriggerStay(Collider other)
    {
        _waiter = other.GetComponent<IWaiter>();
        if (_waiter!=null && isHaveFood() && !_waiter.IsCapacityFull())
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter>=0.2f)
            {
               ResetCounter();
                _waiter.TakeFood(GiveFood());
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IWaiter>()==_waiter)
        {
            _waiter = null;
            ResetCounter();
            
        }
    }

    private void ResetCounter()
    {
        _image.fillAmount = 0;
        _timeCounter = 0;
    }

    public void AddNewFood(Food food)
    {
        food.transform.SetParent(transform.GetChild(_foods.Count));
        _foods.Add(food);
        food.transform.eulerAngles=Vector3.zero;
        food.transform
            .DOLocalJump(Vector3.zero, 1, 1, 0.5f);
        // food.transform.localPosition=Vector3.zero;
    }

    public Food GiveFood()
    {
        var food = _foods[_foods.Count - 1];
        _foods.Remove(food);
        return food;
    }


    public bool IsCapacityFull(int addCapacity)
    {
        if (_foods.Count >= 3 + addCapacity) return true;
        return false;
    }

    public bool isHaveFood()
    {
        if (_foods.Count > 0) return true;
        return false;
    }
}