using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireManTutorial : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseTutorial()
    {
        if (gameObject.activeSelf)
        {
            _saveManager.AreaOpenNextState();
        }
    }
}
