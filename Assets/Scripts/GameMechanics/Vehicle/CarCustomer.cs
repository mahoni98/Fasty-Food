using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCustomer : MonoBehaviour
{
   private Animator _animator;

   void Awake()
   {
       _animator = GetComponent<Animator>();
   }


}
