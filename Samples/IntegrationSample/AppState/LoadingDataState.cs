using Core.States;
using Samples.IntegrationSample.UI.Loading;
using Zenject;

namespace Samples.IntegrationSample.AppState
{
    public class LoadingDataState : BaseState<LoadingPresenter>
    {
        [Inject] 
        private IAppStateChart appStateChart;
        
        public LoadingDataState(DiContainer container) : base(container)
        {
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            presenter.OnLoadingComplete += OnLoadingComplete;
        }

        private void OnLoadingComplete()
        {
            appStateChart.ExecuteStateTrigger(AppStateTrigger.LoadingDataComplete);
        }

        protected override void OnFinish()
        {
            presenter.OnLoadingComplete -= OnLoadingComplete;
            base.OnFinish();
        }
    }
}