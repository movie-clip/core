using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.Configs
{
    public class UIConfig : ScriptableObject
    {
        [SerializeField] 
        private List<ViewConfig> _views;

        [HideInInspector, SerializeField] 
        private string _pathToAssets;

        public List<ViewConfig> Views => _views;

        public string PathToAssets
        {
            get => _pathToAssets;
            set => _pathToAssets = value;
        }
    }
}