using UnityEngine;

namespace Core.ViewManager
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _screenLayer;

        [SerializeField]
        private Transform _windowsLayer;

        private GameObject _lastScreen;
        private GameObject _lastWindow;

        private static ViewManager _instance;

        public static ViewManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ViewManager>();
                }

                return _instance;
            }
        }

        protected void Awake()
        {
            _instance = this;
        }

        public void OpenWindow(string viewId, object options = null)
        {
            GameObject go = Instantiate(Resources.Load("Gui/Windows/" + viewId), _windowsLayer) as GameObject;
            BaseView window = go.GetComponent<BaseView>();
            if(window != null)
            {
                window.Options = options;
            }

            _lastWindow = go;
        }

        public void CloseWindow()
        {
            if (_lastWindow != null)
            {
                GameObject.Destroy(_lastWindow);
            }
        }

        public void SetView(string viewId)
        {
            if(_lastScreen != null)
            {
                GameObject.Destroy(_lastScreen);
            }

            GameObject go = Instantiate(Resources.Load("Gui/Screens/" + viewId), _screenLayer) as GameObject;
            _lastScreen = go;
        }
    }
}
