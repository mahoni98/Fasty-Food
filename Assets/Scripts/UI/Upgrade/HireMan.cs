using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HireMan : MonoBehaviour
{
    [SerializeField] private GameObject _serviceMan;
    [SerializeField] private Button _button;
    [SerializeField] private SaveManager _serviceAreaManager;
    [SerializeField] private StaffData _staffData;

    private void Awake()
    {
        _button.onClick.AddListener(SetServiceMan);
        if (_serviceMan.activeSelf)
        {
            _button.interactable = false;
        }
    }
    
    private void SetServiceMan()
    {
        _staffData.SetServiceManCount();
        _serviceAreaManager.AreaOpenNextState();
        _button.interactable = false;
    }
}
