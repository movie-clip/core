using System;
using System.Collections;
using Core.Actions;
using Core.UnityUtils;
using spector_core.Actions;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace Core.Utils.ActionUtils.Actions
{
    public class GetCommand : BaseCommand
    {
        public GetCommand(string url, string json, IActionParams actionParams, Action<string> complete, Action<string, IActionParams> fail) : base(url, json, actionParams, complete, fail)
        {
        }

        public GetCommand()
        {
        }

        public override void Execute()
        {
            base.Execute();

            CoroutineManager.Instance.StartCoroutine(SendRequest(URL, OnRequestComplete, OnRequestFail));
        }

        private void OnRequestComplete(string data)
        {
            Complete(data);
        }

        private void OnRequestFail(string error)
        {
            Fail(error);
        }

        private IEnumerator SendRequest(string url, Action<string> onComplete, Action<string> onFailed = null)
        {
            var request = new UnityWebRequest(url);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = 10;

            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log($"<color=yellow>[GET]</color><color=red>[FAIL]:</color> URL: {url} Error: {request.error}");
                onFailed?.Invoke(request.error);
            }
            else
            {
                Debug.Log($"<color=yellow>[GET]</color><color=green>[SUCCESS]:</color> URL: {url} Data: {request.downloadHandler.text}");
                onComplete?.Invoke(request.downloadHandler.text);
            }
        }
    }
}