using System;
using Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.IntegrationSample.UI
{
    public class MainLobbyView : BaseView<LobbyViewData>
    {
        public event Action OnUpdateSettingsClicked;
        
        [SerializeField] private Button updateSettingsBtn;
        [SerializeField] private TextMeshProUGUI envLabel;

        protected override void Start()
        {
            base.Start();
            updateSettingsBtn.onClick.AddListener(UpdateSettingsClicked);
        }

        private void UpdateSettingsClicked()
        {
            OnUpdateSettingsClicked?.Invoke();
        }

        protected override void OnContextUpdate(LobbyViewData context)
        {
            base.OnContextUpdate(context);

            envLabel.text = context.ProfileID;
        }

        protected override void OnReleaseResources()
        {
            updateSettingsBtn.onClick.RemoveListener(UpdateSettingsClicked);
            base.OnReleaseResources();
        }
    }
}