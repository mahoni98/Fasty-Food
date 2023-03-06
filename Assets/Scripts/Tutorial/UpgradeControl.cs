using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeControl : MonoBehaviour
{
    [SerializeField] private GameObject _chefBuyArrow;
    [SerializeField] private SaveManager[] _managers;
    [SerializeField] private bool isBuyChef;

    private void Awake()
    {
        _chefBuyArrow.SetActive(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuyChef) return;
   
        if (StaffManager.Instance.stafData.ChefCount>0)
        {
            isBuyChef = true;
            _chefBuyArrow.SetActive(false);
            foreach (var manager in _managers)
            {
                manager.AreaOpenNextState();
            }
        }
    }
}
