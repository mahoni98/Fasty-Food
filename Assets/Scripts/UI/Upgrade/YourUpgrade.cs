using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YourUpgrade : MonoBehaviour
{
    private bool isEnableTrue;
    
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private StaffData _stafData;

    [SerializeField] private Button _moveSpeedButton;
    [SerializeField] private Button _collectSpeedButton;
    [SerializeField] private Button _capacityButton;

    [SerializeField] private TextMeshProUGUI _moveSpeedText;
    [SerializeField] private TextMeshProUGUI _collectSpeedText;
    [SerializeField] private TextMeshProUGUI _capacityText;


    private void Awake()
    {
        _capacityButton.onClick.AddListener(UpgradeCapacity);
        _collectSpeedButton.onClick.AddListener(UpgradeCollectSpeed);
        _moveSpeedButton.onClick.AddListener(UpgradeSpeed);
    }
    
    private void OnEnable()
    {
        if (isEnableTrue)
        {
            MaxControl(1);
        }
    }

    void Start()
    {
        MaxControl(1);
        OrderManager.Instance.OnMoneyChange += MaxControl;
    }

    void UpgradeCapacity()
    {
        _playerData.SetCapacity(20 + (_playerData.Capacity * 120));
        
        CapacityControl();
    }

    void UpgradeSpeed()
    {
        _playerData.SetMovementSpeed(50+(_playerData.MovementSpeed * 100));
        MoveSpeedControl();
    }

    void UpgradeCollectSpeed()
    {
        _playerData.SetCollectSpeed(50 + (_playerData.CollectSpeed * 100));
        CollectSpeedControl();
    }


    public void MaxControl(int  a)
    {
        CapacityControl();
        MoveSpeedControl();
        CollectSpeedControl();
    }
    

    public void CapacityControl()
    {
     
        int needMoney = 20 + (_playerData.Capacity * 120);
        _capacityText.text = _playerData.Capacity + "/" + _playerData.capacityMax;
     
        _capacityButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
        needMoney.ToString();
        if (_playerData.Capacity>=_playerData.capacityMax || OrderManager.Instance.Money<needMoney || _stafData.ChefCount==0)
        {
            _capacityButton.interactable = false;
            return;
        }
        _capacityButton.interactable = true;

    }
    
      public void MoveSpeedControl()
    {
        _moveSpeedText.text = _playerData.MovementSpeed + "/" + _playerData.MoveSpeedMax;
        int needMoney = 50+(_playerData.MovementSpeed * 100);
        _moveSpeedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
       needMoney.ToString();
        if (_playerData.MovementSpeed>=_playerData.MoveSpeedMax || OrderManager.Instance.Money<needMoney  || _stafData.ChefCount==0)
        {
            _moveSpeedButton.interactable = false;
            return;
        }
        _moveSpeedButton.interactable = true;
      
    }
    
      public void CollectSpeedControl()
    {
        _collectSpeedText.text = _playerData.CollectSpeed + "/" + _playerData.collectSpeedMax;
        int needMoney = 50 + (_playerData.CollectSpeed * 100);
        _collectSpeedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
        needMoney.ToString();
        if (_playerData.CollectSpeed>=_playerData.collectSpeedMax || OrderManager.Instance.Money<needMoney  || _stafData.ChefCount==0)
        {
            _collectSpeedButton.interactable = false;
            return;
        }
        _collectSpeedButton.interactable = true;
    
    }
    
    
    
    
}
