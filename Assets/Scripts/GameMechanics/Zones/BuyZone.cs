using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyZone : MonoBehaviour
 {
     [SerializeField] private float _machinePrice;
     [SerializeField] private TextMeshProUGUI _machinePriceText;
     [SerializeField] private float _currentMoney;
     [SerializeField] private Image _image;
     private float _timeCounter;
     private Collider _collider;
     private Camera _cam;
     [SerializeField] private AudioSource _audioSource;


     private void Awake()
     {
         _cam = Camera.main;
     }

     private void Start()
     {
         _machinePriceText.text = _machinePrice.ToString();
         _collider = GetComponent<Collider>();
     }

     private void OnTriggerStay(Collider other)
     {
         if (other.CompareTag("Player") && OrderManager.Instance.Money>=10)
         {
           
             _timeCounter += Time.deltaTime;
             if (_timeCounter<0.1f) return;
             _timeCounter = 0;
             _audioSource.Play();
             _currentMoney += 10;
             _machinePriceText.text = (_machinePrice - _currentMoney).ToString();
             PayMoney(other.transform);
             float totalMoney =_currentMoney / _machinePrice;
             _image.fillAmount =totalMoney;
             if (_image.fillAmount>=1)
             {
                 OrderManager.Instance.audioSource.Play();
                 _collider.enabled = false;
                 OpenMachine();
             }
         }
     }

     private void PayMoney(Transform player)
     {
         OrderManager.Instance.SetMoney(-10);
         Transform moneyUI = CanvasManager.Instance.InstantiateMoneyUI();
         moneyUI.position = _cam.WorldToScreenPoint(player.position);
         Vector3 target = _cam.WorldToScreenPoint(transform.position);
         moneyUI.DOJump(target, 1,1,0.3f)
             .OnComplete((() => Destroy(moneyUI.gameObject)));
     }

  


     private void OpenMachine()
     {
         SaveManager manager =  transform.parent.GetComponent<SaveManager>();
            manager.AreaOpenNextState();
            foreach (var nextArea in     manager.nextAreas)
            {
                nextArea.AreaOpenNextState();
            }
     }
 }

