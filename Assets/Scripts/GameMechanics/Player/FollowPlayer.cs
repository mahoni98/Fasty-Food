using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
   [SerializeField] private Transform _player;
    

   void Awake()
   {
       
   }
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.position;
    }
}
