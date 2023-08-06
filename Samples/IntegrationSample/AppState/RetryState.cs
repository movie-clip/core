using System.Collections;
using Core.States;
using Core.UnityUtils;
using UnityEngine;
using Zenject;

namespace Samples.IntegrationSample.AppState
{
    public class RetryState : BaseState
    {
        [Inject] 
        private IAppStateChart _appStateChart;
        
        public RetryState(DiContainer container) : base(container)
        {
        }

        protected override void OnStart()
        {
            base.OnStart();

            CoroutineManager.Instance.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(3);
            _appStateChart.ExecuteStateTrigger(AppStateTrigger.RetryComplete);
        }
    }
}