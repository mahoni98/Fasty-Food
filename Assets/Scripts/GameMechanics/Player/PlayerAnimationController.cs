using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimationController: MonoBehaviour
    {
        #region Fields

        private Animator animator;
        private PlayerService _playerService;

        #endregion
        
        #region Properties
        
        
        #endregion

        #region Unity Methods

        void Start()
        {
            animator = GetComponent<Animator>();
            _playerService = GetComponent<PlayerService>();
            // InvokeRepeating(nameof(AnimationListener),0f,0.001f);
        }
        

        public void AnimationListener()
        {
            if (Input.GetMouseButton(0))
            {
                if (_playerService.IsHaveFood())
                {
                    ChangeAnim(PlayerAnim.boxWalking);
                    return;
                }
                ChangeAnim(PlayerAnim.walking);
                return;
            }
            
            if (_playerService.IsHaveFood())
            {
                ChangeAnim(PlayerAnim.boxIdle);
                return;
            }
            ChangeAnim(PlayerAnim.idle);

        }
        
        #endregion

        #region Private Methods

        void StopAllAnimations()
        {
            animator.SetBool("Idle",false);
            animator.SetBool("Walk",false);
        }

        #endregion

        #region Public Methods

        public void SetWalkAnimation(float movementSpeed)
        {
            
            animator.SetFloat("WalkingSpeed",movementSpeed);
        }

        public void IdleAnimation()
        {
            if (Input.GetMouseButtonUp(0))
            {
                StopAllAnimations();
            
                animator.SetBool("Idle",true);
            }
        }
        #endregion

        public void ChangeAnim(PlayerAnim playerAnim)
        {
            animator.SetInteger("Anim", (int)playerAnim);
        }
    }
}

public enum PlayerAnim
{
    idle=1,
    walking=2,
    boxWalking=3,
    boxIdle=4,
}