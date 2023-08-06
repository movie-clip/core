using Samples.IntegrationSample.UI;

namespace Samples.IntegrationSample.Modules.Settings
{
    public enum EnvironmentType
    {
        DEV,
        QA,
        LIVE
    }
    
    public interface IGameSettingsDataProvider
    {
        GameSettingsData GetSettings();
    }

    public class GameSettingsData
    {
        public string ProfileID { get; set; }
    }
    
    public class GameSettingsModel : IGameSettingsDataProvider
    {
        public GameSettingsData Settings;
        
        public GameSettingsData GetSettings()
        {
            return Settings;
        }
    }
}