using System;
using Services.Network.Requests;

namespace Samples.IntegrationSample.Actions
{
    [Serializable]
    public class GetAppSettingsActionResponse : NetworkResponse
    {
        public string env;
    }
}