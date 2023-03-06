using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeZone : MonoBehaviour
{
    private float _timeCounter;
   [SerializeField] private float _waitTime;
   [SerializeField] private Image _image;
   [SerializeField] private GameObject _upgradeCanvas;
   private bool _isOpen;
   
   
   private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_isOpen)
        {
            _timeCounter += Time.deltaTime;
            _image.gameObject.SetActive(true);
            _image.fillAmount =_timeCounter/_waitTime;
            if (_image.fillAmount>=1)
            {
                _image.gameObject.SetActive(false);
                _isOpen = true;
               _upgradeCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _image.gameObject.SetActive(false);
            _isOpen = false;
            _timeCounter = 0;
            _image.fillAmount = 0;
        }
    }
}
