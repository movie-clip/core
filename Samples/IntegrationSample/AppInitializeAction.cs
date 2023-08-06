using System;
using Core.Utils.ActionUtils.Actions;

namespace Samples.IntegrationSample
{
    public class AppInitializeAction : IAction
    {
        public event Action<string> OnComplete;
        public event Action<string> OnFail;
        
        public void Execute()
        {
            OnComplete?.Invoke("Complete");
        }

        public void Destroy()
        {
            
        }
    }
}