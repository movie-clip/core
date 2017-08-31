using UnityEngine;

namespace Core
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool IsOverrideOnAwake = true;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var temp = FindObjectOfType<T>();

                    if (temp != null)
                    {
                        _instance = temp;
                    }
                }

                return _instance;
            }
        }
        
        public virtual void Awake()
        {
            if (_instance == null || IsOverrideOnAwake)
                _instance = this as T;
        }
    }
}