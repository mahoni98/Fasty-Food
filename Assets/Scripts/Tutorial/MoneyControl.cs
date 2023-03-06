using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyControl : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private SaveManager _upgradeManager;
    [SerializeField] private int moneyFlag;


    private void Start()
    {
        // OrderManager.Instance.SetMoney(-OrderManager.Instance.Money);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ( OrderManager.Instance.Money>=moneyFlag)
            {
                GetComponent<Collider>().enabled = false;
                foreach (SaveManager nextArea in _saveManager.nextAreas)
                {
                    nextArea.AreaOpenNextState();
                }
                _saveManager.AreaOpenNextState();
                if (_upgradeManager!=null)
                {
                    _upgradeManager.AreaOpenNextState();
                }

            }
        }
    }
}
