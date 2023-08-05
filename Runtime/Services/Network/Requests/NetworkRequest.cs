using System;

namespace Core.Services.Network
{
    public abstract class NetworkRequest : INetworkRequest
    {
        public string Api { get; protected set; }
        public string Json { get; protected set; }
        public bool OverrideApi { get; protected set; }
        public MethodType Method { get; protected set; } = MethodType.Get;

        public NetworkRequest() { }

        public NetworkRequest(string api)
        {
            Api = api;
        }
    }
}