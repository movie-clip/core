using System;
using Core;
using Core.States;
using Core.Utils.ActionUtils.Actions;
using UnityEngine;
using Zenject;

namespace Samples.IntegrationSample.AppState
{
    public interface IAppStateChart
    {
        void ExecuteStateTrigger(AppStateTrigger trigger);
    }
    
    public class AppStateChart: IAppStateChart, IInitializable, IDisposable
    {
        [Inject]
        private StateFactory _stateFactory;
        
        [Inject]
        private DiContainer _diContainer;
        
        private IStrategyManager _strategyManager;
        
        private readonly FiniteStateMachine<AppStateType, AppStateTrigger> _fsm = new();

        // private LoadingDataPresenter _loadingDataPresenter;
        
        public void Initialize()
        {
            Debug.Log("[State] Initialize AppStateChart");

            _strategyManager = new StrategyManager(_diContainer);
            
            // _loadingDataPresenter = new LoadingDataPresenter();
            // _loadingDataPresenter.Start();
            
            _fsm.AddState(AppStateType.Initialize)
                .OnEntryAction(OnEntryInitializeState)
                .AddTransition(AppStateType.LoadingData, AppStateTrigger.AppInitializeComplete);

            _fsm.AddState(AppStateType.LoadingData)
                .OnEntryAction(OnEntryLoadingDataState)
                .AddTransition(AppStateType.AppReady, AppStateTrigger.LoadingDataComplete)
                .AddTransition(AppStateType.WaitForRetry, AppStateTrigger.LoadingDataFailed);

            _fsm.AddState(AppStateType.WaitForRetry)
                .OnEntryAction(OnEntryRetryState)
                .AddTransition(AppStateType.LoadingData, AppStateTrigger.RetryComplete);
            
            _fsm.AddState(AppStateType.AppReady)
                .OnEntryAction(OnEntryAppIdleState);
            
            _fsm.Finalize(AppStateType.Initialize);
        }

        public void ExecuteStateTrigger(AppStateTrigger trigger)
        {
            Debug.Log($"[State] Trigger: {trigger}");
            
            if (_fsm.IsFinalized)
            {
                _fsm.ExecuteTrigger(trigger);
            }
        }
        
        private void OnEntryInitializeState()
        {
            Debug.Log($"[State] InitializeState Start");
            
            var queue = new ActionQueue();
            queue.AddAction(_stateFactory.Create<CoreInitializeAction>());
            queue.AddAction(_stateFactory.Create<AppInitializeAction>());
            queue.Start(() =>
            {
                ExecuteStateTrigger(AppStateTrigger.AppInitializeComplete);
            });
        }
        
        private void OnEntryLoadingDataState()
        {
            Debug.Log($"[State] LoadingDataState Start");
            _strategyManager.ApplyStrategy<LoadingDataState>();
        }
        
        private void OnEntryRetryState()
        {
            Debug.Log($"[State] RetryState Start");
            _strategyManager.ApplyStrategy<RetryState>();
        }
        
        private void OnEntryAppIdleState()
        {
            Debug.Log($"[State] AppReadyState Start");
            _strategyManager.ApplyStrategy<AppReadyState>();
        }

        public void Dispose()
        {
            _fsm.Dispose();
        }
    }
}