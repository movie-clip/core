using Core.UI;
using Zenject;

namespace Samples.IntegrationSample.UI
{
    public class AppReadyPresenter : BasePresenter
    {
        [Inject]
        private MainLobbyPresenter lobbyPresenter;
        
        public override void Start()
        {
            lobbyPresenter.Start();
        }

        public override void Finish()
        {
            lobbyPresenter.Finish();
        }
    }
}