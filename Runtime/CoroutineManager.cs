using UnityEngine;

namespace Core.UnityUtils
{
    [ExecuteInEditMode]
    public class CoroutineManager : BaseMonoBehaviour
    {
        private static CoroutineManager _instance;

        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CoroutineManager>();
                }

                if (_instance == null)
                {
                    GameObject instanceObject = new GameObject(typeof(CoroutineManager).Name);
                    _instance = instanceObject.AddComponent<CoroutineManager>();
                }

                return _instance;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
            // DontDestroyOnLoad(this);
        }
    }
}