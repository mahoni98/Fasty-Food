using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerService : MonoBehaviour,IWaiter
 {

     [SerializeField] private List<Food> _foods;
     [SerializeField] private Transform _handStackPoint;
     [SerializeField] private PlayerData _playerData;
     [SerializeField] private TextMeshProUGUI _capacityInfo;
     private Coroutine _capacityCoroutine;
     [SerializeField] private float _timeCounter;
     [SerializeField] private float _timeFlag;
     [SerializeField] private AudioSource _audio;


     private void Update()
     {
         _capacityInfo.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
     }

     private void OnTriggerStay(Collider other)
     {
         ServiceTable serviceTable = other.GetComponent<ServiceTable>();
         if (CountTime() && serviceTable!=null && IsHaveFood() && !serviceTable.IsFull() &&serviceTable.foodType==GetFood().foodType)
         {
             serviceTable.TakeFood(FoodService());
         }
         
     }

     private void OnTriggerExit(Collider other)
     {
         ServiceTable serviceTable = other.GetComponent<ServiceTable>();
         if (serviceTable!=null)
         {
             _timeCounter = 0;
         }
     }

     public void TakeFood(Food food)
     {
         _audio.Play();
         food.transform.SetParent(_handStackPoint);
         food.transform.localEulerAngles = Vector3.zero;
         DOTween.Kill(food.transform);
         food.transform
             .DOLocalJump(Vector3.up*(_handStackPoint.childCount-1)*0.3f, 1, 1, 0.5f);
         _foods.Add(food);
         if (_capacityCoroutine!=null) StopCoroutine(_capacityCoroutine);
         _capacityCoroutine = StartCoroutine(SetCapacityText());
     }

     public Food FoodService()
     {
       
         Food food = _foods[_foods.Count - 1];
         _foods.Remove(food);
         food.transform.SetParent(null);
         if (_capacityCoroutine!=null) StopCoroutine(_capacityCoroutine);
         _capacityCoroutine = StartCoroutine(SetCapacityText());
         return food;
     }

     public float WaiterCollectSpeed()
     {
         return 1-(_playerData.CollectSpeed*0.1f);
     }


     public bool IsWaiter(bool trash)
     {
         return false;
     }

     public bool IsHaveFood()
     {
         if (_foods.Count > 0) return true;
         return false;
     }

     public List<Food> GetFoods()
     {
         return _foods;
     }

     public bool IsCapacityFull()
     {
         if (_foods.Count >= 3 + _playerData.Capacity) return true;
         return false;
     }

     public Food GetFood()
     {
         return _foods[_foods.Count - 1];
     }

     public IEnumerator SetCapacityText()
     {
         _capacityInfo.text = _foods.Count + " / " + (_playerData.Capacity+3);
         if (_foods.Count==_playerData.Capacity+3)
         {
             _capacityInfo.text = "MAX";
         }
    
         yield return new WaitForSeconds(2f);
         _capacityInfo.text = "";
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

