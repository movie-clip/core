using Core.UnityUtils;
using Core.Utils.ActionUtils.Actions;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Core.Actions.Queue
{
    public class DownloadTextureAction : BaseAction
    {
        private readonly string _url;
        private readonly Action<Texture2D> _complete;
        private readonly Action<string> _fail;

        public DownloadTextureAction(string url, Action<Texture2D> complete, Action<string> fail = null) : base()
        {
            _url = url;
            _complete = complete;
            _fail = fail;
        }

        public override void Execute()
        {
            base.Execute();
            CoroutineManager.Instance.StartCoroutine(SendRequest(_url, OnRequestComplete, OnRequestFail));
        }

        private void OnRequestComplete(Texture2D data)
        {
            _complete?.Invoke(data);
            Complete(string.Empty);
        }

        private void OnRequestFail(string error)
        {
            _fail?.Invoke(error);
            Fail(error);
        }

        private IEnumerator SendRequest(string url, Action<Texture2D> onComplete, Action<string> onFailed = null)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            request.downloadHandler = new DownloadHandlerTexture();
            request.timeout = 60;

            yield return request.SendWebRequest();
            
            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log($"<color=red>[GET][FAIL]: </color> URL: {url} Error: {request.error}");
                onFailed?.Invoke(request.error);
            }
            else
            {
                onComplete?.Invoke(((DownloadHandlerTexture)request.downloadHandler).texture);
            }
        }

        public override void Destroy()
        {
            CoroutineManager.Instance.StopCoroutine(SendRequest(_url, OnRequestComplete, OnRequestFail));
            base.Destroy();
        }
    }
}
