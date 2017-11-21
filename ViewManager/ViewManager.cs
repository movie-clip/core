using Core.ResourceManager;
using Core.ViewManager.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.ViewManager
{
    public struct ViewStruct
    {
        public string LayerId;
        public string SectionId;

        public ViewStruct(string layerId, string sectionId)
        {
            LayerId = layerId;
            SectionId = sectionId;
        }
    }

    public class ViewManager : SingletonMonoBehaviour<ViewManager>, IViewManager
    {
        [SerializeField]
        private List<ViewLayer> _layers;

        private Dictionary<string, ViewLayer> _uiLayersDict;
        private Dictionary<string, ViewStruct> _viewDictionary;

        public void Init()
        {
            _uiLayersDict = new Dictionary<string, ViewLayer>();
            _viewDictionary = new Dictionary<string, ViewStruct>();

            foreach (var layer in _layers)
            {
                _uiLayersDict[layer.name] = layer;
            }
        }

        public BaseView SetView(string viewId, object options = null)
        {
            return SetView(viewId, GetLayerByViewId(viewId), options);
        }

        public BaseView SetViewToLayer(string viewId, string layerId, object options = null)
        {
            return SetView(viewId, GetLayerById(layerId), options);
        }

        private BaseView SetView(string viewId, ViewLayer layer, object options = null)
        {
            if (layer.Current == null)
            {
                CreateView(viewId, options, layer);
            }
            else
            {
                layer.AddViewToLine(viewId, options, 0);
                layer.Current.CloseView();
            }

            return layer.Current;
        }
        
        public void RemoveView(BaseView view)
        {
            var layer = view.Layer;
            layer.RemoveCurrentView();

            //string linker = _viewDictionary[view.name].Linker;
            //if (linker != null)
            //{
            //    ResourcesCache.Clean(linker, true);
            //}

            var viewVo = layer.GetNextView();
            if (viewVo != null)
            {
                SetView(viewVo.ViewId, layer, viewVo.Options);
            }
        }
        
        public void RegisterView(string viewId, string layerId)
        {
            string section = "Windows";

            if (layerId == LayerNames.ScreenLayer)
            {
                section = "Screens";
            }
            else if (layerId == LayerNames.ThreeDLayer)
            {
                section = "GameView";
            }

            _viewDictionary[viewId] = new ViewStruct(layerId, section);
        }

        public ViewLayer GetLayerById(string layerId)
        {
            if (!_uiLayersDict.ContainsKey(layerId))
            {
                throw new Exception("Can't find layer with such id " + layerId);
            }

            return _uiLayersDict[layerId];
        }

        public ViewLayer GetLayerByViewId(string viewId)
        {
            if (!_viewDictionary.ContainsKey(viewId))
            {
                throw new Exception("Can't find view with such id " + viewId);
            }

            string layerKey = _viewDictionary[viewId].LayerId;
            return GetLayerById(layerKey);
        }

        private void CreateView(string viewId, object options, ViewLayer layer)
        {
            if (!_viewDictionary.ContainsKey(viewId))
            {
                throw new Exception("Can't find view with such id " + viewId);
            }

            string section = _viewDictionary[viewId].SectionId;
            if (!ResourcesCache.IsResourceLoaded(viewId))
            {
                ResourcesCache.SetupGuiResourcesCache(viewId, section);
            }
            
            var view = Instantiate(ResourcesCache.GetObject<BaseView>(section, viewId));
            view.Options = options;
            view.Layer = layer;
            view.gameObject.SetActive(true);
            view.gameObject.name = viewId;
            view.transform.SetParent(layer.transform, false);
            layer.AddView(view);
        }
    }
}
