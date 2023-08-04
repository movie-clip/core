using System;
using System.Collections.Generic;

namespace Core.Utils.ActionUtils.Actions
{
    public class ActionQueue
    {
        public bool IgnoreFail;
        private Action<string> _onQueueFail;
        private Action _onQueueComplete;

        private readonly List<IAction> _actions;

        private int _currentIndex;
        private bool _isPaused;
        
        private bool _isDestroyed;

        public ActionQueue()
        {
            _actions = new List<IAction>();
        }

        public void AddAction(IAction actionObject)
        {
            _actions.Add(actionObject);
        }

        public void AddActionAt(IAction actionObject, int index)
        {
            _actions.Insert(index,actionObject);
        }

        public void Start()
        {
            _currentIndex = 0;
            ExecuteAction();
        }

        public void Start(Action onComplete = null, Action<string> onQueueFail = null)
        {
            _onQueueComplete = onComplete;
            _onQueueFail = onQueueFail;
            Start();
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            if (_isPaused)
            {
                _isPaused = false;
                ExecuteAction();
            }
        }
        
        private void ExecuteAction()
        {
            if (_isPaused)
            {
                return;
            }

            if (_currentIndex < _actions.Count)
            {
                var delegateObject = _actions[_currentIndex];
                delegateObject.OnComplete += OnActionExecuteComplete;
                delegateObject.OnFail += OnActionExecuteFail;
                delegateObject.Execute();
            }
            else
            {
                _onQueueComplete?.Invoke();
                Destroy();
            }
        }

        private void OnActionExecuteComplete(string result)
        {
            var action = _actions[_currentIndex];
            action.OnComplete -= OnActionExecuteComplete;
            action.OnFail -= OnActionExecuteFail;

            _currentIndex++;
            ExecuteAction();
        }

        private void OnActionExecuteFail(string error)
        {
            if (IgnoreFail)
            {
                OnActionExecuteComplete(String.Empty);
                return;
            }
            
            _onQueueFail?.Invoke(error);
            Destroy();
        }

        public void Destroy()
        {
            if (_isDestroyed)
            {
                return;
            }

            foreach (var action in _actions)
            {
                action.OnComplete -= OnActionExecuteComplete;
                action.OnFail -= OnActionExecuteFail;
                action.Destroy();
            }

            _actions.Clear();
            _onQueueFail = null;
            _isDestroyed = true;
        }
    }
}