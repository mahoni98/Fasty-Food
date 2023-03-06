using System;
using Player.Stack;
using UnityEngine;
using UnityEngine.UI;

namespace FoodZone
{
    public class FoodZoneController: MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image fillImage;
        [SerializeField] private Transform createPosition;
        [SerializeField] private GameObject foodPrefab;
        [SerializeField] private float createSpeed = 0;
        
        #endregion
        
        #region Properties
        
        
        #endregion

        #region Unity Methods

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!PlayerStackController.Instance.isStackFull)
                {
                    if (fillImage.fillAmount < 1)
                    {
                        fillImage.fillAmount += createSpeed * Time.deltaTime;
                    }
                    else
                    {
                        GameObject food= Instantiate(foodPrefab, createPosition.position, Quaternion.identity);
                        PlayerStackController.Instance.AddObjectToStack(food);
                        fillImage.fillAmount = 0;
                    }
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                fillImage.fillAmount = 0;
            }
        }

        #endregion

        #region Private Methods

        

        #endregion

        #region Public Methods

        

        #endregion
    }
  

}