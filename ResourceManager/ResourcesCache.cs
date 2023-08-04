using System.Collections.Generic;
using Core.Configs;
using Core.UI;
using UnityEngine;

namespace Core.ResourceManager
{
    public static class ResourcesCache
    {
        private const string UiConfigPath = "Configs/UIConfig";
        private const string AssetConfigPath = "Configs/AssetConfig";

        private static readonly Dictionary<string, BaseView> _map = new ();

        private static UIConfig _uiConfig;
        private static AssetConfig _assetConfig;
        
        private static void Initialize()
        {
            if (_uiConfig == null)
            {
                _assetConfig = Resources.Load<AssetConfig>(AssetConfigPath);
                _uiConfig = Resources.Load<UIConfig>(UiConfigPath);
                if (_uiConfig == null)
                {
                    Debug.LogError("No UIConfig by path: " + UiConfigPath);
                    return;
                }

                for (int i = 0; i < _uiConfig.Views.Count; i++)
                {
                    _map.Add(_uiConfig.Views[i].Id, _uiConfig.Views[i].View);
                }
            }
        }

        public static BaseView GetViewById(string viewId)
        {
            Initialize();

            if (!_map.ContainsKey(viewId))
            {
                Debug.LogError("No view found with Id: " + viewId);
                return null;
            }

            return _map[viewId];
        }

        public static AssetConfig AssetConfig
        {
            get { return _assetConfig; }
        }
    }
}