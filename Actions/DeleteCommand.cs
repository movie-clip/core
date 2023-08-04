using System;
using System.Collections;
using System.Text;
using Core.Actions;
using Core.UnityUtils;
using Core.Utils.ActionUtils.Actions;
using spector_core.Actions;
using UnityEngine;
using UnityEngine.Networking;

namespace Actions
{
    public class DeleteCommand : BaseCommand
    {
        public DeleteCommand(string url, string json, IActionParams actionParams, Action<string> complete, Action<string, IActionParams> fail) : base(url, json, actionParams, complete, fail)
        {
        }

        public DeleteCommand()
        {
        }

        public override void Execute()
        {
            base.Execute();

            CoroutineManager.Instance.StartCoroutine(SendRequest(URL, OnRequestComplete, OnRequestFail));
        }

        private void OnRequestComplete(string result)
        {
            Complete(result);
        }

        private void OnRequestFail(string error)
        {
            Fail(error);
        }

        private IEnumerator SendRequest(string url, Action<string> onComplete, Action<string> onFailed = null)
        {
            var request = new UnityWebRequest(url, "DELETE");
            
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("content-type", "application/json");

            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log($"<color=yellow>[DELETE]</color><color=red>[FAIL]: </color> URL: {url} Error: {request.error}");
                onFailed?.Invoke(request.error);
            }
            else
            {
                Debug.Log($"<color=yellow>[DELETE]</color><color=green>[SUCCESS]: </color> URL: {url} Data: {request.downloadHandler.text}");
                onComplete?.Invoke(request.downloadHandler.text);
            }
        }
    }
}