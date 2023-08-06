
namespace Samples.IntegrationSample.Modules.Settings
{
    public class GameSettingsSignal
    {
        public class Updated
        {
            public Updated(GameSettingsData data)
            {
                Data = data;
            }

            public GameSettingsData Data { get; }
        }
    }
}