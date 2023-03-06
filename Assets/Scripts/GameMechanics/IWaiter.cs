using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface IWaiter
 {
     void TakeFood(Food food);
     Food FoodService();
     float WaiterCollectSpeed();

     bool IsCapacityFull();

     Food GetFood();

     bool IsWaiter(bool trash);

     bool IsHaveFood();

     List<Food> GetFoods();
 }

