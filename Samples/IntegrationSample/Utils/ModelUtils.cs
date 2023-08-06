using Samples.IntegrationSample.Actions;
using Samples.IntegrationSample.Modules.Settings;

namespace Samples.IntegrationSample.Utils
{
    public static class ModelUtils
    {
        public static GameSettingsData Convert(this GetAppSettingsActionParams actionParams)
        {
            return new GameSettingsData
            {
                ProfileID = actionParams.profileID
            };
        }
    }
}