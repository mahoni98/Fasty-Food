using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookZone : MonoBehaviour
 {
     private Machine _machine;
     private IWaiter _waiter;
     private float _timeCounter;
     [SerializeField] private Image _image;

     private void Awake()
     {
         _machine = transform.parent.GetComponent<Machine>();
     }
     
     
     
     private void OnTriggerEnter(Collider other)
     {
       IWaiter  iWaiter = other.GetComponent<IWaiter>();
         if (iWaiter!=null && !iWaiter.IsWaiter(false))
         {
             _waiter = iWaiter;
         }
     }

     private void OnTriggerStay(Collider other)
     {
      
         if (_waiter!=null && !_waiter.IsCapacityFull() && !_waiter.IsWaiter(false))
         {
             _image.gameObject.SetActive(true);
             _timeCounter += Time.deltaTime;
             _image.fillAmount = _timeCounter / _waiter.WaiterCollectSpeed();
             if (_image.fillAmount>=1)
             {
                 ResetCounter();
                 _waiter.TakeFood(GiveFood());
                
             }
         }
     }

     private void OnTriggerExit(Collider other)
     {
         
         IWaiter  iWaiter = other.GetComponent<IWaiter>();
         if (iWaiter!=null && !iWaiter.IsWaiter(false))
         {
             _waiter = null;
             ResetCounter();
         }
     }
     
     
     private void ResetCounter()
     {
         _image.gameObject.SetActive(false);
         _image.fillAmount = 0;
         _timeCounter = 0;
     }
     
     
     public Food GiveFood()
     {

         return _machine.SpawnFood();
     }
     
     
 }

