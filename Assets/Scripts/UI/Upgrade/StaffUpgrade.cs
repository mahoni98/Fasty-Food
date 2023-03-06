using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaffUpgrade : MonoBehaviour
{

    [SerializeField] private StaffData _staffData;
    private bool isEnableTrue;
    
    [Header("Chef")]
    [SerializeField] private Button _chefHiringButton;
    [SerializeField] private Button _chefCapacityButton;
    [SerializeField] private Button _chefSpeedButton;
    [SerializeField] private TextMeshProUGUI _chefCountText;
    [SerializeField] private TextMeshProUGUI _chefCapacityText;
    [SerializeField] private TextMeshProUGUI _chefSpeedText;
    
    [Header("Waiter")]
    [SerializeField] private Button _waiterHiringButton;
    [SerializeField] private Button _waiterCapacityButton;
    [SerializeField] private Button _waiterSpeedButton;
    [SerializeField] private TextMeshProUGUI _waiterCountText;
    [SerializeField] private TextMeshProUGUI _waiterCapacityText;
    [SerializeField] private TextMeshProUGUI _waiterSpeedText;

    private void OnEnable()
    {
        if (isEnableTrue)
        {
         Control();
        }
    }

    void Start()
    {
        Control();
        isEnableTrue = true;
        StaffManager.Instance.OnChangeChef += Control;
        OrderManager.Instance.OnMoneyChange += ControlMax;
        
        _chefHiringButton.onClick.AddListener(ChefHiring);
        _chefCapacityButton.onClick.AddListener(ChefCapacityUpdate);
        _chefSpeedButton.onClick.AddListener(ChefSpeedUpdate);
        
        _waiterHiringButton.onClick.AddListener(WaiterHiring);
        _waiterCapacityButton.onClick.AddListener(WaiterCapacityUpdate);
        _waiterSpeedButton.onClick.AddListener(WaiterSpeedUpdate);
    }

    void Control()
    {
        ChefHiringControl();
        ChefCapacityControl();
        ChefSpeedControl();
        
        WaiterHiringControl();
        WaiterCapacityControl();
        WaiterSpeedControl();
    }
    
    
        
    void ControlMax(int a)
    {
        Control();
    }
    
    
    
    void ChefHiring()
    {
        int chefNeedMoney = 20 + ( (_staffData.ChefCount%5) * 150);
        StaffManager.Instance.HiringChef(-chefNeedMoney);
        OrderManager.Instance.audioSource.Play();
        ChefHiringControl();
    }    
    void WaiterHiring()
    {
        OrderManager.Instance.audioSource.Play();
        int waiterNeedMoney = 200 + ( _staffData.WaiterCount * 200);
        StaffManager.Instance.HiringWaiter(-waiterNeedMoney);
        WaiterHiringControl();
    }

    void ChefCapacityUpdate()
    {
        int chefNeedMoney=0;
        switch (_staffData.ChefCapacity)
        {
            case 0 : chefNeedMoney = 80;
                break;
            
            case 1 : chefNeedMoney = 180;
                break;
            
            case 2 : chefNeedMoney = 280;
                break;
        }
        OrderManager.Instance.SetMoney(-chefNeedMoney);
        _staffData.SetChefCapacity();
        ChefCapacityControl();
    }
    
    void ChefSpeedUpdate()
    {
        int chefNeedMoney=0;
        switch (_staffData.ChefSpeed)
        {
            case 0 : chefNeedMoney = 80;
                break;
            
            case 1 : chefNeedMoney = 160;
                break;
            
            case 2 : chefNeedMoney = 320;
                break;
            
            case 3 : chefNeedMoney = 640;
                break;
            
            case 4 : chefNeedMoney = 1280;
                break;

        }
        OrderManager.Instance.SetMoney(-chefNeedMoney);
        _staffData.SetChefSpeed();
        ChefSpeedControl();
    }
    
    void WaiterCapacityUpdate()
    {
        int waiterNeedMoney=0;
        switch (_staffData.WaiterCapacity)
        {
            case 0 : waiterNeedMoney = 80;
                break;
            
            case 1 : waiterNeedMoney = 200;
                break;
            
            case 2 : waiterNeedMoney = 320;
                break;
        }
        OrderManager.Instance.SetMoney(-waiterNeedMoney);
        _staffData.SetWaiterCapacity();
        WaiterCapacityControl();
    }
    
    void WaiterSpeedUpdate()
    {
        int waiterNeedMoney=0;
        switch (_staffData.WaiterCollectSpeed)
        {
            case 0 : waiterNeedMoney = 100;
                break;
            
            case 1 : waiterNeedMoney = 250;
                break;
            
            case 2 : waiterNeedMoney = 400;
                break;
        }
        OrderManager.Instance.SetMoney(-waiterNeedMoney);
        _staffData.SetWaiterCollectSpeed();
        WaiterSpeedControl();
    }
    
    

    void ChefHiringControl()
    {
        int chefNeedMoney = 20 + ( (_staffData.ChefCount%5) * 150);
        _chefCountText.text = _staffData.ChefCount + "/" + 9;
        _chefHiringButton.transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>().text=  chefNeedMoney.ToString();
        if ( _staffData.ChefCount>=12 || StaffManager.Instance.machines.Count==0 || OrderManager.Instance.Money<chefNeedMoney)
        {
            _chefHiringButton.interactable = false;
            return;
        }

        _chefHiringButton.interactable = true;
    }

    void ChefCapacityControl()
    {
        int chefNeedMoney=0;
        switch (_staffData.ChefCapacity)
        {
            case 0 : chefNeedMoney = 80;
                break;
            
            case 1 : chefNeedMoney = 180;
                break;
            
            case 2 : chefNeedMoney = 280;
                break;
        }
        _chefCapacityText.text =   "CAPACITY " + _staffData.ChefCapacity + "/" + _staffData.chefCapacityMax;
        _chefCapacityButton.transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>().text=chefNeedMoney.ToString();
        if (_staffData.ChefCapacity>=_staffData.chefCapacityMax || OrderManager.Instance.Money<chefNeedMoney  || _staffData.ChefCount==0)
        {
            _chefCapacityButton.interactable = false;
            return;
        }

        _chefCapacityButton.interactable = true;
    }

    void ChefSpeedControl()
    {
        int chefNeedMoney=0;
        switch (_staffData.ChefSpeed)
        {
            case 0 : chefNeedMoney = 80;
                break;
            
            case 1 : chefNeedMoney = 160;
                break;
            
            case 2 : chefNeedMoney = 320;
                break;
            
            case 3 : chefNeedMoney = 640;
                break;
            
            case 4 : chefNeedMoney = 1280;
                break;

        }
        _chefSpeedText.text =  "COOKING SPEED " + _staffData.ChefSpeed + "/" + _staffData.chefSpeedMax;
        _chefSpeedButton.transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>().text=  chefNeedMoney.ToString();
        if (_staffData.ChefSpeed>=_staffData.chefSpeedMax || OrderManager.Instance.Money<chefNeedMoney  || _staffData.ChefCount==0)
        {
            _chefSpeedButton.interactable = false;
            return;
        }

        _chefSpeedButton.interactable = true;
    }
    
    void WaiterHiringControl()
    {
        int waiterNeedMoney = 200 + (_staffData.WaiterCount * 200);
        _waiterCountText.text = _staffData.WaiterCount + "/" + 2;
        _waiterHiringButton.transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>().text=  waiterNeedMoney.ToString();
        if (_staffData.WaiterCount>=2
            || OrderManager.Instance.Money<waiterNeedMoney
            || _staffData.ChefCount==0
            || _staffData.ServiceManCount<=_staffData.WaiterCount)
        {
            _waiterHiringButton.interactable = false;
            return;
        }

        _waiterHiringButton.interactable = true;
    }

    void WaiterCapacityControl()
    {
        int waiterNeedMoney=0;
        switch (_staffData.WaiterCapacity)
        {
            case 0 : waiterNeedMoney = 80;
                break;
            
            case 1 : waiterNeedMoney = 200;
                break;
            
            case 2 : waiterNeedMoney = 320;
                break;
        }
        _waiterCapacityText.text =   "CAPACITY " +_staffData.WaiterCapacity + "/" + _staffData.waiterCollectMax;
        _waiterCapacityButton.transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>().text=  waiterNeedMoney.ToString();
        if (_staffData.WaiterCapacity>=_staffData.waiterCollectMax
            || OrderManager.Instance.Money<waiterNeedMoney
            || _staffData.WaiterCount==0)
        {
            _waiterCapacityButton.interactable = false;
            return;
        }

        _waiterCapacityButton.interactable = true;
    }

    void WaiterSpeedControl()
    {
        int waiterNeedMoney=0;
        switch (_staffData.WaiterCollectSpeed)
        {
            case 0 : waiterNeedMoney = 100;
                break;
            
            case 1 : waiterNeedMoney = 250;
                break;
            
            case 2 : waiterNeedMoney = 400;
                break;
        }
        _waiterSpeedText.text =  "SPEED " + _staffData.WaiterCollectSpeed + "/" + _staffData.waiterSpeedMax;
        _waiterSpeedButton.transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>().text=  waiterNeedMoney.ToString();
        if (_staffData.WaiterCollectSpeed>=_staffData.waiterSpeedMax || OrderManager.Instance.Money<waiterNeedMoney ||_staffData.WaiterCount==0)
        {
            _waiterSpeedButton.interactable = false;
            return;
        }
        _waiterSpeedButton.interactable = true;
      
    }
}
