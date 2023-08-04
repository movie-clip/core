using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.LocalData.Action
{
    public class LocalDataAction
    {
        public void SaveJsonData<T>(T data, string name = "")
        {
            string fileName = string.IsNullOrEmpty(name) ? data.GetType().ToString() : name;
            string filePath = Application.persistentDataPath + $"/{fileName}.json";
            string json = JsonUtility.ToJson(data);

            if (!File.Exists(filePath))
            {
                var fs = new FileStream(filePath, FileMode.Create);
                fs.Dispose();
            }

            File.WriteAllText(filePath, json);
        }

        public T LoadJsonData<T>(string name = "")
        {
            string fileName = string.IsNullOrEmpty(name) ? typeof(T).ToString() : name;
            string filePath = Application.persistentDataPath + $"/{fileName}.json";

            if (!File.Exists(filePath))
            {
                return default(T);
            }

            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        
        public AssetBundle LoadData(string assetIndex)
        {
            string filePath = Application.persistentDataPath + "/bundle_" + $"{assetIndex}";
            if (!File.Exists(filePath))
            {
                return null;
            }
            
            return AssetBundle.LoadFromFile(filePath);
        }

        public void SaveData(string assetIndex, byte[] bytes)
        {
            string filePath = Application.persistentDataPath + "/bundle_" + $"{assetIndex}";
            File.WriteAllBytes(filePath, bytes);
        }

        public void RemoveData(string prefix)
        {
            var files = Directory.GetFiles(Application.persistentDataPath);
            List<string> toRemove = new List<string>();

            foreach (string file in files)
            {
                if (file.Contains(prefix))
                {
                    toRemove.Add(file);
                }
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                File.Delete(toRemove[i]);
            }
        }
    }
}
