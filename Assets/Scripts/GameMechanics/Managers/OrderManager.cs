using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameAnalyticsSDK.Setup;
using Managers.Singleton;
using UnityEngine;

 public class OrderManager : SingletonManager<OrderManager>
 {
     public List<Food> _restaurantsMaterials;
     public List<Food> _cafeMaterials;
     public GameObject restaurantCarPrefab,cafeCarPrefab,bikeOrderPrefab;
     public List<GameObject> restaurantOrders;
     public List<GameObject> cafeOrders;
     public List<GameObject> bikeOrders;
     [SerializeField] private MoneyArea _moneyAreaRestaurant;
     [SerializeField] private MoneyArea _moneyAreaCafe;
     public Bike bike;
     public delegate void MoneyEvent(int money);

     public AudioSource audioSource;

     public MoneyEvent OnMoneyChange;
     

     public int Money
     {
         get => PlayerPrefs.GetInt("Money", 2000000);
     }

     private void Start()
     {
         StartCoroutine(CreateNewRestaurantOrder());
         StartCoroutine(CreateNewCafeOrder());
         StartCoroutine(CreateNewBikeOrder());
     }

     public void SetMoney(int amount)
     {
         
         int newMoney = Money + amount;
         
         if (newMoney<0)
         {
             newMoney = 0;
         }
         PlayerPrefs.SetInt("Money",newMoney);
         
         OnMoneyChange?.Invoke(newMoney);
     }

     public void SetNewMaterial(AreaType areaType,GameObject food)
     {
         switch (areaType)
         {
           case AreaType.Restaurant:
               _restaurantsMaterials.Add(food.GetComponent<Food>());
           break;
           
           case AreaType.Cafe:
               _cafeMaterials.Add(food.GetComponent<Food>());
           break;
         }
     }

     public IEnumerator CreateNewRestaurantOrder()
     {
         yield return new WaitForSeconds(0.5f);
         while (true)
         {
             if (restaurantOrders.Count < 3 && _restaurantsMaterials.Count>0)
             {
                 restaurantOrders.Add(Instantiate(restaurantCarPrefab));
             }
             yield return new WaitForSeconds(1f);
         }
     }
     
     public IEnumerator CreateNewBikeOrder()
     {
         yield return new WaitForSeconds(0.5f);
         while (true)
         {
             if (bikeOrders.Count < 3 && _restaurantsMaterials.Count>0 && bike.gameObject.activeSelf)
             {
                 bikeOrders.Add(Instantiate(bikeOrderPrefab));
             }
             yield return new WaitForSeconds(0.5f);
         }
     }
     
     public IEnumerator CreateNewCafeOrder()
     {
         yield return new WaitForSeconds(0.5f);
         while (true)
         {
             if (cafeOrders.Count < 3 && _cafeMaterials.Count>0)
             {
                 cafeOrders.Add(Instantiate(cafeCarPrefab));
                 
             }
             yield return new WaitForSeconds(1f);
         }
     }

     public void PayMoney(AreaType areaType)
     {
         if (areaType==AreaType.Restaurant)
         {
         _moneyAreaRestaurant.AddMoney();
          return;   
         }
         _moneyAreaCafe.AddMoney();
     }

 }


public enum AreaType
{
    Restaurant,
    Cafe,
    Bike,
}

