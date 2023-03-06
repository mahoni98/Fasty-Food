using System;
using Player.Animation;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement: MonoBehaviour
    {
        #region Fields

        [SerializeField] private Joystick joystick;
        [SerializeField] private PlayerData _playerData;
        
        private Rigidbody rb;
        private PlayerAnimationController animationController;
        
        #endregion
        
        #region Properties
        
        
        #endregion

        #region Unity Methods

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animationController = GetComponent<PlayerAnimationController>();
        }

        private void Update()
        {
            animationController.AnimationListener();
        }

        void FixedUpdate()
        {
             MovePlayer();
        }

        #endregion

        #region Private Methods

        void MovePlayer()
        {
            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;

            Vector3 direction = new Vector3(horizontal, 0, vertical);
            
            rb.velocity = direction * (_playerData.MovementSpeed*0.1f+5);
            RotatePlayer(direction);
            
            float magnitude = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            float animationSpeed = Mathf.Clamp(magnitude, 0f, 1f);
         
            animationController.SetWalkAnimation(animationSpeed* (_playerData.MovementSpeed*0.1f+5)*0.50f);
        }

        void RotatePlayer(Vector3 dir)
        {
            if (dir != Vector3.zero)
            {
                transform.rotation=Quaternion.LookRotation(dir);
            }
        }
        #endregion

        #region Public Methods

        

        #endregion
    }
  

}