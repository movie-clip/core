using System;
using Core.Actions;
using Core.Data.Time;
using Core.Services.Network;
using Samples.IntegrationSample.Modules.Settings;
using Samples.IntegrationSample.Utils;
using Zenject;

namespace Samples.IntegrationSample.Actions
{
    public class GetAppSettingsAction : AbstractAction<GetAppSettingsActionRequest, GetAppSettingsActionResponse, GetAppSettingsActionParams>
    {
        [Inject] private IGameSettingsService gameSettingsService;
        [Inject] private IGameSettingsDataProvider dataProvider;

        private GameSettingsData prev;
        
        public GetAppSettingsAction(INetworkService networkService, SignalBus signalBus, IDateTimeProvider dateTimeProvider) : base(networkService, signalBus, dateTimeProvider)
        {
        }

        public override bool CanExecute(GetAppSettingsActionParams actionParams, DateTime timeStamp)
        {
            return true;
        }

        protected override GetAppSettingsActionRequest CreateNetworkRequest(GetAppSettingsActionParams actionParams)
        {
            return new GetAppSettingsActionRequest();
        }

        protected override void UpdateModel(GetAppSettingsActionParams args, DateTime timeStamp)
        {
            prev = dataProvider.GetSettings();
            gameSettingsService.UpdateSettings(args.Convert());
        }

        protected override void OnActionFail(string message, GetAppSettingsActionParams response, DateTime timeStamp)
        {
            gameSettingsService.UpdateSettings(prev);
        }
    }
}