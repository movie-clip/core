using System;
using Actions;
using Core.Actions;
using Core.Network.Data;
using Core.Utils.ActionUtils.Actions;
using spector_core.Actions;
using UnityEngine;
using Zenject;

namespace Core.Services.Network
{
    public interface INetworkService
    {
        void CreateRequest<TRequest, TType, TParams>(TRequest request, TParams actionParams, Action<string> onComplete,
            Action<string, IActionParams> onFail) 
            where TRequest : INetworkRequest
            where TType : BaseCommand, new()
            where TParams : IActionParams;
    }
    
    public class NetworkService : INetworkService
    {
        [Inject]
        private INetworkDataProvider _networkDataProvider;

        public void CreateRequest<TRequest, TCommand, TParams>(TRequest request, TParams actionParams,
            Action<string> onComplete, Action<string, IActionParams> onFail) where TRequest : INetworkRequest where TCommand : BaseCommand, new() where TParams : IActionParams
        {
            string url = request.Api;
            
            if (_networkDataProvider.CurrentEnv != null && !string.IsNullOrEmpty(_networkDataProvider.CurrentEnv.api) && !request.OverrideApi)
            {
                url = string.Format("{0}{1}", _networkDataProvider.CurrentEnv.api, request.Api);    
            }

            Debug.Log($"<color=green>[NetworkGateway]</color><color=yellow>[{request.Method}]:</color>{url}\n{request.Json}");
            
            var action = new TCommand();
            action.Init(url, request.Json, actionParams, onComplete, onFail);
            action.Execute();
        }
    }
}