using System;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.IntegrationSample.UI.Loading
{
    public class LoadingView : BaseView
    {
        public event Action OnMockResponseClick;
        
        [SerializeField] private Button mockResponseButton;

        protected override void Start()
        {
            base.Start();
            mockResponseButton.onClick.AddListener(OnMockClick);
        }

        private void OnMockClick()
        {
            OnMockResponseClick?.Invoke();
        }

        protected override void OnReleaseResources()
        {
            mockResponseButton.onClick.RemoveListener(OnMockClick);
            base.OnReleaseResources();
        }
    }
}