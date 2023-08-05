using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Core.Actions;
using UnityEditor;
using UnityEngine;

namespace Core.Utils
{
    public class LoadAndCreateAssetBundle : EditorWindow
    {
        private string _url;
        
        [MenuItem("Spiral/Load Asset Bundle")]
        public static void Init()
        {
            LoadAndCreateAssetBundle window = CreateInstance<LoadAndCreateAssetBundle>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
            window.ShowPopup();
        }

        public void OnGUI()
        {
//            "http://cloud-test.spiral.technology/storage/asset5kj7sa79ee/mac-cones"
                
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            {
                EditorGUILayout.LabelField("Url:", EditorStyles.wordWrappedLabel);
                string url = EditorGUILayout.TextField( _url, EditorStyles.boldLabel);
                _url = url;
            }
            EditorGUILayout.EndHorizontal();
            
            
            GUILayout.Space(40);
            
            EditorGUILayout.BeginHorizontal();
            {
                if (!string.IsNullOrEmpty(_url))
                {
                    if (GUILayout.Button("Download"))
                    {
                        DownloadBundleAction action = new DownloadBundleAction(_url, Complete);
                        action.Execute();
                    }
                }
                
                
                if (GUILayout.Button("Cancel"))
                {
                    this.Close();
                }
            }
            EditorGUILayout.EndHorizontal();
            
            
        }
        
        private void Complete(AssetBundle obj)
        {
            var mainPrefab = obj.LoadAsset<GameObject>("model.prefab");
            Instantiate(mainPrefab);
            
            this.Close();
        }


        [MenuItem("Spiral/Load Local Asset Bundle")]
        public static void LoadLocalBundle()
        {
            AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "wsa-mainbundle"));
            Debug.Log($"Bundle: {myLoadedAssetBundle}");

            var assets = myLoadedAssetBundle.LoadAllAssets();
            foreach (Object asset in assets)
            {
                Debug.Log(asset);
                Instantiate(asset);
            }
            
            // myLoadedAssetBundle.Unload(true);
        }
    }
}

