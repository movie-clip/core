using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.ResourceManager
{
    public class ResourcesCache
    {
        private static Dictionary<string, GameObject> _map = new Dictionary<string, GameObject>();

        public static bool IsResourceLoaded(string viewId)
        {
            return _map.ContainsKey(viewId);
        }

        public static void SetupGuiResourcesCache(string viewId, string sectionId)
        {
            var go = Resources.Load<GameObject>("Gui/" + sectionId + "/" + viewId);
            _map.Add(viewId, go);
        }

        public static void SetupResourcesCache(string viewId, string sectionId)
        {
            var go = Resources.Load<GameObject>(sectionId + "/" + viewId);
            _map.Add(viewId, go);
        }

        public static T GetObject<T>(string section, string viewId)
        {
            if(!_map.ContainsKey(viewId))
            {
                throw new Exception("[ResourcesCache] Can't find view with such id " + viewId);
            }

            var go = _map[viewId];
            return go.GetComponent<T>();
        }
    }
}
