using System.Collections.Generic;
using Core.Network.Data;

namespace Core.Services.Network.Data
{
    public class EndPointData : INetworkResponse
    {
        public string device_id;
        public string account_id;
        public List<ProjectData> projects;
    }
}