using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;

public class CanvasChange : MonoBehaviour
{
    [SerializeField] private GameObject openCanvas, closeCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openCanvas!=null)
            {
                openCanvas.SetActive(true);
            }
           
            closeCanvas.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && tag.Equals("BikeArea"))
        {
            if (openCanvas!=null)
            {
                openCanvas.SetActive(false);
            }
            closeCanvas.SetActive(true);
        }
    }
}
