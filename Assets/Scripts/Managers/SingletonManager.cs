using UnityEngine;

namespace Managers.Singleton
{
    public class SingletonManager<T> : MonoBehaviour where T:SingletonManager<T>
    {
        #region Fields
        
        public static T Instance;

        #endregion

        #region Unity Methods

        protected  virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this as T;
        }

        #endregion

    }

}