using Core.Actions;
using Core.Services.Network;
using Samples.IntegrationSample.Modules.Settings;

namespace Samples.IntegrationSample.Actions
{
    public class GetAppSettingsActionParams : IActionParams
    {
        public string profileID;

        public GetAppSettingsActionParams(string profile)
        {
            profileID = profile;
        }

        public GetAppSettingsActionParams()
        {
        }

        public IActionParams Clone()
        {
            return new GetAppSettingsActionParams(profileID);
        }
    }
    public class GetAppSettingsActionRequest : NetworkRequest
    {
        public GetAppSettingsActionRequest() : base($"game/settings")
        {
            Method = MethodType.Get;
        }
    }
}