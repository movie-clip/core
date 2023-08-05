using System;

namespace Core.Utils.ActionUtils.Actions
{
    public interface IAction
    {
        void Execute();
        
        void Destroy();
        
        event Action<string> OnComplete;
        
        event Action<string> OnFail;
    }
}