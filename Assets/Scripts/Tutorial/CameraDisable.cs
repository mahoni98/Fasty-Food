using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DisableCam),2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableCam()
    {
        gameObject.SetActive(false);
    }
}
