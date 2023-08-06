using System;
using System.Collections.Generic;
using Core.ResourceManager;
using UnityEngine;
using Zenject;

namespace Core.UI
{
    public struct ViewStruct
    {
        public string LayerId;

        public ViewStruct(string layerId)
        {
            LayerId = layerId;
        }
    }

    public class ViewManager : SingletonBaseMonoBehaviour<ViewManager>
    {
        [SerializeField] private List<ViewLayer> _layers;

        private Dictionary<string, ViewLayer> _uiLayersDict;
        private Dictionary<string, ViewStruct> _viewDictionary;
        private Dictionary<string, BaseView> _viewCache;

        public void Initialize()
        {
            _uiLayersDict = new Dictionary<string, ViewLayer>();
            _viewDictionary = new Dictionary<string, ViewStruct>();
            _viewCache = new Dictionary<string, BaseView>();

            foreach (var layer in _layers)
            {
                _uiLayersDict[layer.name] = layer;
            }
        }

        public BaseView GetView(string viewId)
        {
            return _viewCache.ContainsKey(viewId) ? _viewCache[viewId] : null;
        }

        public BaseView SetView(string viewId)
        {
            Debug.Log($"SetView {viewId}");
            return SetView(viewId, GetLayerByViewId(viewId));
        }

        private BaseView SetView(string viewId, ViewLayer layer)
        {
            if (_viewCache.ContainsKey(viewId))
            {
                 return _viewCache[viewId];
            }

            var view = CreateView(viewId, layer);
            if (view != null)
            {
                _viewCache.Add(viewId, view);
            }

            return view;
        }

        public void RemoveView(string viewId)
        {
            if (_viewCache.ContainsKey(viewId))
            {
                RemoveView(_viewCache[viewId]);
            }
        }

        public void RemoveAllViews(string exception = null)
        {
            var viewsToDelete = new BaseView[_viewCache.Count];
            _viewCache.Values.CopyTo(viewsToDelete, 0);

            foreach (var view in viewsToDelete)
            {
                if (!string.IsNullOrEmpty(exception) && exception == view.name)
                {
                    continue;
                }

                _viewCache.Remove(view.name);
                view.Layer.RemoveView(view);
            }
        }

        public void RemoveView(BaseView view)
        {
            List<string> viewsToDelete = new List<string>();
            foreach (var item in _viewCache)
            {
                if (item.Value == view)
                {
                    viewsToDelete.Add(item.Key);
                }
            }

            foreach (var item in viewsToDelete)
            {
                _viewCache.Remove(item);
            }

            view.Layer.RemoveView(view);
        }

        private BaseView CreateView(string viewId, ViewLayer layer)
        {
            if (!_viewDictionary.ContainsKey(viewId))
            {
                throw new Exception("Can't find view with such id " + viewId);
            }
            
            // var view = Instantiate(ResourcesCache.GetViewById(viewId), layer.transform, false);
            var view = Instantiate(ResourcesCache.GetViewById(viewId), layer.transform);
            view.Layer = layer;
            view.name = viewId;
            view.gameObject.SetActive(true);

            layer.AddView(view);

            return view;
        }

        public void RegisterView(string viewId, string layerId)
        {
            _viewDictionary[viewId] = new ViewStruct(layerId);
        }

        public ViewLayer GetLayerById(string layerId)
        {
            if (!_uiLayersDict.ContainsKey(layerId))
            {
                throw new Exception("Can't find layer with such id " + layerId);
            }

            return _uiLayersDict[layerId];
        }

        private ViewLayer GetLayerByViewId(string viewId)
        {
            if (!_viewDictionary.ContainsKey(viewId))
            {
                throw new Exception("Can't find view with such id " + viewId);
            }

            string layerKey = _viewDictionary[viewId].LayerId;
            return GetLayerById(layerKey);
        }
    }
}