using System;
using System.Collections.Generic;

namespace Core.Utils.ActionUtils.Actions
{
    public class ParallelQueue
    {
        public bool IgnoreFail;
        private Action<string> _onQueueFail;
        private Action _onQueueComplete;

        private readonly List<IAction> _actions;

        private int _currentIndex;
        private bool _isPaused;
        
        private bool _isDestroyed;

        public ParallelQueue()
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

        private void ExecuteAction()
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                _actions[i]?.Execute();
            }
            
            _onQueueComplete?.Invoke();
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
                action.Destroy();
            }

            _actions.Clear();
            _onQueueFail = null;
            _isDestroyed = true;
        }
    }
}