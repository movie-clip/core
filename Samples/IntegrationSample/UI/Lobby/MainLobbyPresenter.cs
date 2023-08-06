using Core.UI;
using Samples.IntegrationSample.Actions;
using Samples.IntegrationSample.Modules.Settings;
using Zenject;

namespace Samples.IntegrationSample.UI
{
    public class MainLobbyPresenter : BasePresenter<MainLobbyView>
    {
        [Inject] private IGameSettingsDataProvider dataProvider;
        protected override string ViewId => "MainLobbyView";

        protected override void OnStart()
        {
            base.OnStart();
            SignalBus.Subscribe<GameSettingsSignal.Updated>(OnAppSettingUpdated);
            View.OnUpdateSettingsClicked += OnUpdateSettingsClicked;
            
            UpdateView(dataProvider.GetSettings());
        }

        private void OnUpdateSettingsClicked()
        {
            var rnd = new System.Random();
            var pr = rnd.Next(0, 100);
            GameActionLocator.GetAppSettingsAction.Execute(new GetAppSettingsActionParams(pr.ToString()));
        }

        private void OnAppSettingUpdated(GameSettingsSignal.Updated signal)
        {
            UpdateView(signal.Data);
        }

        private void UpdateView(GameSettingsData data)
        {
            View.SetContext(new LobbyViewData(data.ProfileID));
        }
        
        public override void Finish()
        {
            SignalBus.TryUnsubscribe<GameSettingsSignal.Updated>(OnAppSettingUpdated);
            View.OnUpdateSettingsClicked -= OnUpdateSettingsClicked;
            base.Finish();
        }
    }
}