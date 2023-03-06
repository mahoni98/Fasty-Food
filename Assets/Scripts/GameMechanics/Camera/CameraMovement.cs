using UnityEngine;

namespace Cam
{
    public class CameraMovement: MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform player;
        [SerializeField] private float movementSpeed = 0;
        
        private Vector3 distance;
        
        #endregion
        
        #region Properties
        
        
        #endregion

        #region Unity Methods

        void Start()
        {
            distance = transform.position - player.position;
        }
        
        void FixedUpdate()
        {
             FollowPlayer();
        }

        #endregion

        #region Private Methods

        void FollowPlayer()
        {
            transform.position=Vector3.Slerp(transform.position,player.position+distance,movementSpeed*Time.deltaTime);
        }

        #endregion

        #region Public Methods

        

        #endregion
    }
  

}