using System;
using Actions;
using Core.Actions;
using Core.Network.Data;
using Core.Services.Network.Data;
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
        
        void SetEndPoint(EndPointData endPoint);
        void SetEnv(EnvironmentData envData);
    }
    
    public class NetworkService : INetworkService
    {
        [Inject]
        private NetworkModel _model;

        public void CreateRequest<TRequest, TCommand, TParams>(TRequest request, TParams actionParams,
            Action<string> onComplete, Action<string, IActionParams> onFail) where TRequest : INetworkRequest where TCommand : BaseCommand, new() where TParams : IActionParams
        {
            var url = request.Api;
            if (!request.OverrideApi)
            {
                url = string.Format("{0}{1}", _model.CurrentEnv.api, request.Api);
            }

            Debug.Log($"<color=green>[NetworkGateway]</color><color=yellow>[{request.Method}]:</color>{url}\n{request.Json}");
            
            var action = new TCommand();
            action.Init(url, request.Json, actionParams, onComplete, onFail);
            action.Execute();
        }
        
        public void SetEndPoint(EndPointData endPoint)
        {
            _model.endPoint = endPoint;
        }
        
        public void SetEnv(EnvironmentData envData)
        {
            _model.currentEnv = envData;
        }
    }
}