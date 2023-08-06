using Zenject;

namespace Samples.IntegrationSample.Modules.Settings
{
    public interface IGameSettingsService
    {
        void UpdateSettings(GameSettingsData data);
    }

    public class GameSettingsService : IGameSettingsService
    {
        [Inject] private SignalBus signalBus;
        [Inject] private GameSettingsModel model;
        
        public void UpdateSettings(GameSettingsData data)
        {
            model.Settings = data;
            signalBus.Fire(new GameSettingsSignal.Updated(data));
        }
    }
}