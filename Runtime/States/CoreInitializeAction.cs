using Core.Modules.Loading.Signals;
using Core.Services.Network.Signals;
using Core.Utils.ActionUtils.Actions;
using Zenject;

namespace Core.States
{
    public class CoreInitializeAction : BaseAction
    {
        [Inject] 
        private SignalBus _signalBus;

        public override void Execute()
        {
            DeclareMessages();
            Complete();
        }

        private void DeclareMessages()
        {
            _signalBus.DeclareSignal<NetworkSignals.EndPointReceived>();

            _signalBus.DeclareSignal<LoadingSignals.LoadingFinishedSignals>();
        }
    }
}