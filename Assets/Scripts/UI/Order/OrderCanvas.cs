using System.Collections;
using System.Collections.Generic;
using Managers.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class OrderCanvas : SingletonManager<OrderCanvas>
{
    public Transform restaurantPanel;
    public Transform cafePanel;
    public Transform bikePanel;
    public GameObject orderBoxPrefab;
    public Image orderImage;
 
    public OrderBox SetOrder(AreaType areaType)
    {
        Transform panel;
        if (areaType== AreaType.Cafe)
        {
            panel = cafePanel;
        }
        else if (areaType== AreaType.Restaurant)
        {
            panel = restaurantPanel;
        }
        else
        {
            panel = bikePanel;
        }
        OrderBox orderBox =Instantiate(orderBoxPrefab,panel).GetComponent<OrderBox>();
        orderBox.transform.SetSiblingIndex(2);
        
        return orderBox;
    }
}
