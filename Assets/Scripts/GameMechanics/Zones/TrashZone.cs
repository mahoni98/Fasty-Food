using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TrashZone : MonoBehaviour
{

    private Animator _animator;
    [SerializeField] private Transform _trashPoint;
    private List<IWaiter> _iWaiter;
    [SerializeField] private float _timeCounter;
    [SerializeField] private float _timeFlag;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _iWaiter= new List<IWaiter>();
    }

  

    private void OnTriggerEnter(Collider other)
    {
       IWaiter iWaiter = other.GetComponent<IWaiter>();
       if (iWaiter!=null)
        {
            _iWaiter.Add(iWaiter);
            _animator.SetInteger("Anim",1);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (_iWaiter.Count>0 && CountTime())
        {
            foreach (IWaiter waiter in _iWaiter)
            {
                if (waiter.IsHaveFood())
                {
                    DestroyFood(waiter.FoodService());
                    if (waiter.IsWaiter(true));
                }
            }
          
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IWaiter iWaiter = other.GetComponent<IWaiter>();
        if (iWaiter!=null)
        {
            _iWaiter.Remove(iWaiter);
            _timeCounter = 0;
            _animator.SetInteger("Anim",2);
         
        }
        
      
    }

    private void DestroyFood(Food food )
    {
        food.transform.DOJump(_trashPoint.position, 1.1f, 1, 0.5f)
            .OnComplete((() =>
            {
                Destroy(food.gameObject);
            }));
    }

    private bool CountTime()
    {
        _timeCounter += Time.deltaTime;
        if (_timeCounter>=_timeFlag)
        {
            _timeCounter = 0;
            return true;
        }
        
        return false;
    }
}
