using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Utils
{
    public enum Environment
    {
        DEV,
        QA,
        LIVE
    }
    
    [System.Serializable]
    public class EnvironmentConfig
    {
        public Environment Env;
        public string API;
    }
    
    public class EnvironmentConfigurator : ScriptableObject
    {
        public Environment Environment = Environment.DEV;
        public string EDITOR_ID = "Unity_Editor_ID";
        
        public EnvironmentConfig[] Environments;

        public EnvironmentConfig GetEnvironmentConfig()
        {
            return Environments.FirstOrDefault(x => x.Env == Environment);
        }
        
        public string DeviceID
        {
            get
            {
                if (Application.isEditor)
                {
                    return EDITOR_ID;
                }
                
                return SystemInfo.deviceUniqueIdentifier;
            }
        }
    }
}