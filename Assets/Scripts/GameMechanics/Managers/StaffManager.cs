using System.Collections;
using System.Collections.Generic;
using Managers.Singleton;
using Unity.VisualScripting;
using UnityEngine;

public class StaffManager : SingletonManager<StaffManager>
{
    public List<Machine> machines;
    public StaffData stafData;
    public SaveManager[] _waiters;

 
    public delegate void ChefEvent();

    public ChefEvent OnChangeChef;

    public void AddMachine(Machine machine)
    {
        machines.Add(machine);
    }

    public void AddChef()
    {
       stafData.SetChefCount();
        OnChangeChef?.Invoke();
    }

    public void HiringChef(int money)
    {
        machines[0].AddChef();
        machines.Remove(machines[0]);
        OrderManager.Instance.SetMoney(money);
        AddChef();
    }
    
    
    
    public void HiringWaiter(int money)
    {
        _waiters[stafData.WaiterCount].AreaOpenNextState();
        stafData.SetWaiterCount();
        OrderManager.Instance.SetMoney(money);
    
    }
    
    
    
    
    
}
