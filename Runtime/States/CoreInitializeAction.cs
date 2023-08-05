using System;
using Core.Modules.Loading.Signals;
using Core.Services.Network.Signals;
using Core.Utils.ActionUtils.Actions;
using Zenject;

namespace Core.States
{
    public class CoreInitializeAction : IAction
    {
        public event Action<string> OnComplete;
        public event Action<string> OnFail;
        
        [Inject] 
        private SignalBus _signalBus;
        
        public void Execute()
        {
            DeclareMessages();
            
            OnComplete?.Invoke("Complete");
        }

        public void Destroy()
        {
        }

        private void DeclareMessages()
        {
            _signalBus.DeclareSignal<NetworkSignals.EndPointReceived>();

            _signalBus.DeclareSignal<LoadingSignals.LoadingFinishedSignals>();
        }
    }
}