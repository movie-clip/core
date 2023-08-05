using System;
using Actions;
using Core.Data.Time;
using Core.Network;
using Core.Services.Network;
using Core.Utils.ActionUtils.Actions;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Core.Actions
{
    public abstract class AbstractAction<TMessageRequest, TMessageResponse, TParams> where TMessageRequest : INetworkRequest where TParams : IActionParams, new() where TMessageResponse : INetworkResponse
    {
        protected INetworkService networkService;
        protected SignalBus signalBus;
        protected IDateTimeProvider dateTimeProvider;
        
        [Inject]
        public AbstractAction(INetworkService networkService, SignalBus signalBus, IDateTimeProvider dateTimeProvider)
        {
            this.networkService = networkService;
            this.signalBus = signalBus;
            this.dateTimeProvider = dateTimeProvider;
        }
        
        public void Execute(TParams actionParams)
        {
            DateTime timeStamp = dateTimeProvider.UtcNow;
            if (!CanExecute(actionParams, timeStamp))
            {
                Debug.LogError($"Error during execution of { GetType().Name }");
                return;
            }
            
            UpdateModel(actionParams, dateTimeProvider.UtcNow);
            SendNetworkRequest(CreateNetworkRequest(actionParams), actionParams);
        }

        public abstract bool CanExecute(TParams actionParams, DateTime timeStamp);
        
        protected abstract TMessageRequest CreateNetworkRequest(TParams actionParams);
        
        protected abstract void UpdateModel(TParams args, DateTime timeStamp);

        protected virtual void OnActionSuccess(TMessageResponse response, DateTime timeStamp)
        {
        }
        
        protected virtual void OnActionFail(string message, TParams response, DateTime timeStamp)
        {
        }

        protected virtual void SendNetworkRequest(TMessageRequest networkRequest, TParams actionParams)
        {
            if (networkRequest.Method == MethodType.Post)
            {
                networkService.CreateRequest<TMessageRequest, PostCommand, TParams>(networkRequest, (TParams) actionParams.Clone(), OnComplete, OnFail);
                return;
            }
            
            if (networkRequest.Method == MethodType.Patch)
            {
                networkService.CreateRequest<TMessageRequest, PatchCommand, TParams>(networkRequest, (TParams) actionParams.Clone(), OnComplete, OnFail);
                return;
            }
            
            if (networkRequest.Method == MethodType.Delete)
            {
                networkService.CreateRequest<TMessageRequest, DeleteCommand, TParams>(networkRequest, (TParams) actionParams.Clone(), OnComplete, OnFail);
                return;
            }
            
            if (networkRequest.Method == MethodType.Get)
            {
                networkService.CreateRequest<TMessageRequest, GetCommand, TParams>(networkRequest, (TParams) actionParams.Clone(), OnComplete, OnFail);
                return;
            }
        }

        private void OnComplete(string json)
        {
            var result = JsonUtility.FromJson<TMessageResponse>(json);
            OnActionSuccess(result, dateTimeProvider.UtcNow);
        }
        
        private void OnFail(string message, IActionParams actionParams)
        {
            OnActionFail(message, (TParams) actionParams, dateTimeProvider.UtcNow);
        }
    }
}