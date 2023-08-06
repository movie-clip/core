using System;
using Core.UI;
using Samples.IntegrationSample.Modules.Settings;
using Zenject;

namespace Samples.IntegrationSample.UI.Loading
{
    public class LoadingPresenter : BasePresenter<LoadingView>
    {
        public event Action OnLoadingComplete;
        
        [Inject] private IGameSettingsService gameSettingsService;
        
        protected override string ViewId => "LoadingView";

        protected override void OnStart()
        {
            base.OnStart();
            
            View.OnMockResponseClick += OnMockResponseClick;
        }

        private void OnMockResponseClick()
        {
            gameSettingsService.UpdateSettings(new GameSettingsData { ProfileID = "-1"});
            OnLoadingComplete?.Invoke();
        }

        protected override void OnFinish()
        {
            View.OnMockResponseClick -= OnMockResponseClick;
        }
    }
}