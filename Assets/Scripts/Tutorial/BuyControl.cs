using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyControl : MonoBehaviour
{   
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private int foodCountFlag;
    private int _foodCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>()!=null)
        {
            _foodCount++;
            if (_foodCount>=foodCountFlag)
            {
                GetComponent<Collider>().enabled = false;
                foreach (SaveManager nextArea in _saveManager.nextAreas)
                {
                    nextArea.AreaOpenNextState();
                }
                _saveManager.AreaOpenNextState();
            }
        }
    }
}
