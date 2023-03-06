using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }
    
    void Start()
    {
        transform.LookAt(transform.position - _cam.transform.rotation * Vector3.back, _cam.transform.rotation * Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
