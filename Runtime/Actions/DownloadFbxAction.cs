using System;
using System.Collections;
using Core.UnityUtils;
using Core.Utils.ActionUtils.Actions;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.Actions
{
    public class DownloadFbxAction : BaseAction
    {
        private readonly string _url;
        private readonly Action<byte[]> _complete;

        public DownloadFbxAction(string url, Action<byte[]> complete, Action<string> fail) : base(null, fail)
        {
            _url = url;
            _complete = complete;
        }

        public override void Execute()
        {
            base.Execute();

            CoroutineManager.Instance.StartCoroutine(SendRequest(_url, OnRequestComplete, OnRequestFail));
        }

        private void OnRequestComplete(byte[] data)
        {
            _complete?.Invoke(data);
        }

        private void OnRequestFail(string error)
        {
            Fail(error);
        }

        private IEnumerator SendRequest(string url, Action<byte[]> onComplete, Action<string> onFailed = null)
        {
            var request = new UnityWebRequest(url);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = 30;

            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log($"<color=red>[GET][FAIL]: </color> URL: {url} Error: {request.error}");
                onFailed?.Invoke(request.error);
            }
            else
            {
                Debug.Log($"<color=grey>[GET][SUCCESS]:</color>\n{request.downloadHandler.text}");
                onComplete?.Invoke(request.downloadHandler.data);
            }
        }
    }
}