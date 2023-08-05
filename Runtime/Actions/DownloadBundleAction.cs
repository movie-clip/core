using Core.UnityUtils;
using Core.Utils.ActionUtils.Actions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Core.Actions
{
    public class DownloadBundleAction : BaseAction
    {
        private readonly string _url;
        public readonly Action<AssetBundle> _onComplete;

        public DownloadBundleAction(string url, Action<AssetBundle> complete)
        {
            _url = url;
            _onComplete = complete;
        }

        public override void Execute()
        {
            base.Execute();
            CoroutineManager.Instance.StartCoroutine(SendRequest(_url, OnRequestComplete, OnRequestFail));
        }

        private void OnRequestComplete(AssetBundle bundle)
        {
            _onComplete?.Invoke(bundle);
            Complete(string.Empty);
        }

        private void OnRequestFail(string error)
        {
            Fail(error);
        }

        private IEnumerator SendRequest(string url, Action<AssetBundle> onComplete, Action<string> onFailed = null)
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
            Debug.Log($"<color=yellow>[DownloadBundleAction]: </color>{url}");

            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log($"<color=red>[DownloadBundleAction] [Fail]: </color>{request.error}");
                onFailed?.Invoke(request.error);
            }
            else
            {
                Debug.Log($"<color=green>[DownloadBundleAction] Downloaded: </color>");
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                onComplete?.Invoke(bundle);
            }
        }
    }
}
