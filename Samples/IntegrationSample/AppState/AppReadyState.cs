using Core.States;
using Samples.IntegrationSample.UI;
using Zenject;

namespace Samples.IntegrationSample.AppState
{
    public class AppReadyState : BaseState<AppReadyPresenter>
    {
        public AppReadyState(DiContainer container) : base(container)
        {
        }

        protected override void OnStart()
        {
            base.OnStart();
            
        }
    }
}