using System;
using System.Collections.Generic;

namespace Core.Network.Data
{
    [Serializable]
    public class ProjectData
    {
        public string name;
        public string slug;
        public List<EnvironmentData> envs;
    }
}