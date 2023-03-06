using System.Collections.Generic;
using DG.Tweening;
using Managers.Singleton;
using UnityEngine;

namespace Player.Stack
{
    public class PlayerStackController: SingletonManager<PlayerStackController>
    {
        #region Fields

        [SerializeField] private Transform stackPosition;
        [SerializeField] private float stackDistance = 0;
        
        private List<GameObject> playerStack = new List<GameObject>();
        private bool _isStackFull = false;
        
        #endregion
        
        #region Properties

        public bool isStackFull => _isStackFull;

        
        #endregion

        #region Unity Methods

        void Start()
        {
             
        }
        
        void Update()
        {
             
        }

        #endregion

        #region Private Methods

        void MoveObjectAndDestroy(Transform obj,Vector3 movementPos)
        {
            obj.DOMove(movementPos, 0.5f).OnComplete((() => obj.gameObject.SetActive(false)));
        }


        void RepositionStack()
        {
            for (int i = 0; i < playerStack.Count; i++)
            {
                if (i == 0)
                {
                    playerStack[i].transform.position=stackPosition.position;
                }
                else
                {
                    playerStack[i].transform.position=playerStack[playerStack.Count-1].transform.position + (Vector3.up * stackDistance);
                }
               
            }
        }
        #endregion

        #region Public Methods

        public void AddObjectToStack(GameObject obj)
        {
            obj.transform.parent = stackPosition;
            
            if (playerStack.Count == 0)
            {
                obj.transform.DOLocalJump(stackPosition.localPosition, 0.5f,1,0.5f);
            }
            else
            {
                obj.transform.DOLocalJump(playerStack[playerStack.Count-1].transform.localPosition + (Vector3.up * stackDistance), 0.5f,1,0.5f);
            }
            playerStack.Add(obj);
            
            if (playerStack.Count == 3)
            {
                _isStackFull = true;
            }
        }

        public void RemoveObjectFromStack(string objTag,Vector3 movementPos)
        {
            for (int i = playerStack.Count - 1; i >= 0; i--)
            {
                if (playerStack[i].transform.CompareTag(objTag))
                {
                    GameObject obj= playerStack[i];
                    playerStack.Remove(playerStack[i]);
                    obj.transform.parent = null;
                    
                    MoveObjectAndDestroy(obj.transform,movementPos);
                    
                    break;
                }
            }
            
            if (playerStack.Count < 3)
            {
                _isStackFull = false;
            }
        }
        #endregion
    }
  

}