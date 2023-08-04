using UnityEngine;

namespace Core
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        private bool _release;
        
        protected virtual void Awake() { }
        
        protected virtual void Start() { }

        protected virtual void Update() { }

        private void OnApplicationQuit()
        {
            _release = true;
        }

        protected virtual void OnDestroy()
        {
            if (!_release)
            {
                OnReleaseResources();
            }
            
            _release = true;
        }

        protected virtual void OnReleaseResources() { } 
    }
}