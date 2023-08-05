using System;
using Core.Actions;

namespace Core.Utils.ActionUtils.Actions
{
    public class BaseAction : IAction
    {
        public event Action<string> OnComplete;
        public event Action<string> OnFail;

        public BaseAction() { }

        public BaseAction(Action<string> complete, Action<string> fail)
        {
            OnComplete = complete;
            OnFail = fail;
        }
        
        public void Init(Action<string> complete, Action<string> fail)
        {
            OnComplete = complete;
            OnFail = fail;
        }
        
        public virtual void Execute() { }

        protected virtual void Complete(string result = "")
        {
            OnComplete?.Invoke(result);
        }

        protected virtual void Fail(string error = "")
        {
            OnFail?.Invoke(error);
        }
        
        public virtual void Destroy() { }
    }

    public class BaseAction<T> : BaseAction where T : IActionParams
    {
        public T Data;
        
        public BaseAction(T data, Action<string> complete, Action<string> fail) : base(complete, fail)
        {
            Data = data;
        }
        
    }
}