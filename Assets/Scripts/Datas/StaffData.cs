using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffData", menuName = "ScriptableObjects/Datas/StaffData")]
public class StaffData : ScriptableObject
{
    public int chefCapacityMax;
    public int chefSpeedMax;
    public int waiterSpeedMax;
    public int waiterCollectMax;
    public int ChefCapacity => PlayerPrefs.GetInt("ChefCapacity", 0);
    
    public int ChefSpeed => PlayerPrefs.GetInt("ChefSpeed", 0);

    public int WaiterCapacity => PlayerPrefs.GetInt("WaiterCapacity", 0);

    public int WaiterCollectSpeed => PlayerPrefs.GetInt("WaiterCollectSpeed", 0);

    public int ChefCount => PlayerPrefs.GetInt("ChefCount", 0);

    public int WaiterCount => PlayerPrefs.GetInt("WaiterCount", 0);
    
    
    public int ServiceManCount => PlayerPrefs.GetInt("ServiceCount", 0);
    
    
    
    public void SetChefCapacity()
    {
        PlayerPrefs.SetInt("ChefCapacity", ChefCapacity + 1);
    }
    
    public void SetChefSpeed()
    {
        PlayerPrefs.SetInt("ChefSpeed", ChefSpeed + 1);
    }

    public void SetWaiterCapacity()
    {
        PlayerPrefs.SetInt("WaiterCapacity", WaiterCapacity + 1);
    }

    public void SetWaiterCollectSpeed()
    {
        PlayerPrefs.SetInt("WaiterCollectSpeed", WaiterCollectSpeed + 1);
    }

    public void SetChefCount()
    {
        PlayerPrefs.SetInt("ChefCount", ChefCount + 1);
    }

    public void SetWaiterCount()
    {
        PlayerPrefs.SetInt("WaiterCount", WaiterCount + 1);
    }
    
    
    public void SetServiceManCount()
    {
        PlayerPrefs.SetInt("ServiceCount", ServiceManCount + 1);
    }
    
    
    
    
 
    
}