using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ServiceTable : MonoBehaviour
{
    public FoodType foodType;
    public Transform _stackPoint;

    private void Awake()
    {
        transform.parent.GetComponent<Table>().serviceTables.Add(this);
    }
    
    public void TakeFood(Food food)
    {
        food.transform.SetParent(_stackPoint);
        int index = food.transform.GetSiblingIndex();
        Vector3 target=Vector3.zero;
        food.transform.localEulerAngles=Vector3.zero;
        target.y = (index / 6) * 0.3f;
        float z = index * 0.2f;
        target.z = z % 1.2f;
        food.transform.DOLocalJump(target,1,1,0.3f);
    }
    
    public bool IsFull()
    {
        if (_stackPoint.childCount>=12)
        {
            return true;
        }
        return false;
    }
}
