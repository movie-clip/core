using System;
using System.Collections.Generic;

namespace Core
{
    public class FiniteStateMachine<TState, TTrigger> : IDisposable
    {
        private readonly IDictionary<TState, StateData> _states = new Dictionary<TState, StateData>();
        private readonly StateData _anyState = new StateData(default(TState));

        private StateData _currentState;
        private bool _finalized;
		
        public bool IsFinalized
        {
            get { return _finalized; }
        }
		
        public event Action<TState, TTrigger> OnStateTransitionNotPossible = delegate { };
        
        public TState CurrentState
        {
            get
            {
                CheckForNotFinalized();
                return _currentState.State;
            }
        }
        
        public IAnyStateData AnyState
        {
            get
            {
                CheckForFinalized();
                return _anyState;
            }
        }
        
        public IStateData AddState(TState state)
        {
            //Debug.Log($"[FSM] Add state {state.ToString()}");
			
            CheckForFinalized();

            var stateData = new StateData(state);
            _states.Add(state, stateData);
            return stateData;
        }
        
        public void Finalize(TState initialState)
        {
            //Debug.Log($"[FSM] Finalizing state machine setup with initial state {initialState}");
			
            CheckForFinalized();

            if (!_states.ContainsKey(initialState))
            {
                throw new ArgumentException("InitialState is not registered");
            }

            _finalized = true;

            ChangeState(new TransitionData(initialState, null));
        }
        
        public void ExecuteTrigger(TTrigger trigger)
        {
            //Debug.Log($"[FSM] Executing trigger {trigger}");
			
            CheckForNotFinalized();
            if (_anyState.HasTransitionForTrigger(trigger))
            {
                //Debug.Log($"[FSM] doing any state transition for trigger {trigger}");
                ChangeState(_anyState.GetTransitionForTrigger(trigger));
            }
            else if (_currentState.HasTransitionForTrigger(trigger))
            {
                //Debug.Log($"[FSM] doing specific state transition from current state {_currentState.State} for trigger {trigger}");
                ChangeState(_currentState.GetTransitionForTrigger(trigger));
            }
            else
            {
                //Debug.Log($"[FSM] no state transition available for trigger {trigger}, current state {_currentState.State} and any state");
                OnStateTransitionNotPossible(_currentState.State, trigger);
            }
        }
        
        public void Dispose()
        {
            _states.Clear();
            _currentState = null;
            _finalized = false;
        }
        
        private void CheckForFinalized()
        {
            //Debug.Log($"[FSM] check for finalized. Is finalized: {_finalized}");
			
            if (_finalized)
            {
                throw new InvalidOperationException("State machine was already finalized.");
            }
        }
        
        private void CheckForNotFinalized()
        {
            //Debug.Log($"[FSM] check for NOT finalized. Is NOT finalized: {!_finalized}");
			
            if (!_finalized)
            {
                throw new InvalidOperationException("State machine is not finalized.");
            }
        }
        
        private void ChangeState(TransitionData transitionData)
        {
            //Debug.Log($"[FSM] change state");
			
            if (_currentState != null)
            {
                // Debug.Log($"[FSM] changing state from State {_currentState.State} to state {transitionData.TargetState} with transition action {transitionData.TransitionAction != null}");
				
                if (_currentState.ExitAction != null)
                {
                    _currentState.ExitAction();
                }
            }

            if (transitionData.TransitionAction != null)
            {
                transitionData.TransitionAction();
            }

            _currentState = _states[transitionData.TargetState];

            if (_currentState.EntryAction != null)
            {
                _currentState.EntryAction();
            }
        }
        
        public interface IStateData
        {
            TState State { get; }
            IStateData OnEntryAction(Action action);
            IStateData OnExitAction(Action action);
            IStateData AddTransition(TState state, TTrigger trigger, Action transitionAction = null);
        }
        
        public interface IAnyStateData
        {
            IStateData AddTransition(TState state, TTrigger trigger, Action transitionAction = null);
        }
        
        private class StateData : IStateData, IAnyStateData
        {
            private readonly TState _state;
            private readonly Dictionary<TTrigger, TransitionData> _transitionList =
                new Dictionary<TTrigger, TransitionData>();
			
            public Action EntryAction;
            public Action ExitAction;

            public StateData(TState state)
            {
                _state = state;
            }
			
            public TState State { get {return _state; } }

            public IStateData OnEntryAction(Action action)
            {
                EntryAction = action;
                return this;
            }

            public IStateData OnExitAction(Action action)
            {
                ExitAction = action;
                return this;
            }

            public IStateData AddTransition(TState state, TTrigger trigger, Action transitionAction = null)
            {
                _transitionList.Add(trigger, new TransitionData(state, transitionAction));
                return this;
            }

            public bool HasTransitionForTrigger(TTrigger trigger)
            {
                return _transitionList.ContainsKey(trigger);
            }

            public TransitionData GetTransitionForTrigger(TTrigger trigger)
            {
                TransitionData transitionData;
                _transitionList.TryGetValue(trigger, out transitionData);
                return transitionData;
            }
        }
        
        private struct TransitionData
        {
            public readonly TState TargetState;
            public readonly Action TransitionAction;

            public TransitionData(TState targetState, Action transitionAction)
            {
                TargetState = targetState;
                TransitionAction = transitionAction;
            }
        }
    }
}