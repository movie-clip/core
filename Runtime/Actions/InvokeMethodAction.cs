using System;
using Zenject;

namespace Core.Utils.ActionUtils.Actions
{
    public class InvokeMethodAction : BaseAction
    {
        private readonly Action _method;

        public InvokeMethodAction(Action method, Action<string> complete) : base(complete, null)
        {
            _method = method;
        }

        public override void Execute()
        {
            _method?.Invoke();
            Complete(string.Empty);
        }
    }

    public class InvokeMethodAction<T> : BaseAction
    {
        private readonly Action _method;
        private SignalBus _messageHubService;

        public InvokeMethodAction(Action method, SignalBus hub) : base(null, null)
        {
            _method = method;
            _messageHubService = hub;

            _messageHubService.Subscribe<T>(OnMessageReceived);
        }

        private void OnMessageReceived(T message)
        {

        }

        public override void Execute()
        {
            _method?.Invoke();
            Complete(string.Empty);
        }
    }
}