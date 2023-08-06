using System;
using System.Collections.Generic;
using Zenject;

namespace Core.States
{
    public interface IStrategyManager
    {
        void ApplyStrategy<T>() where T : BaseState;
    }
    
    public class StrategyManager : IStrategyManager
    {
        private DiContainer _diContainer;

        private BaseState _currentStrategy;

        [Inject]
        public StrategyManager(DiContainer container)
        {
            _diContainer = container;
        }

        public void ApplyStrategy<T>() where T : BaseState
        {
            _currentStrategy?.Finish();
            _currentStrategy = _diContainer.Instantiate<T>();
            _currentStrategy.Start();
        }
    }
}