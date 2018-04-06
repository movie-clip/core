using System.Collections.Generic;
using Core.ViewManager;
using UnityEngine;

namespace Core.Gui.Config
{
    public class UIConfig : ScriptableObject
    {
        [SerializeField] 
        private List<ViewConfig> _views;


        public List<ViewConfig> Views
        {
            get { return _views; }
        }
    }
}