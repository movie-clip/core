using Zenject;

namespace Samples.IntegrationSample.UI.Loading
{
    public class LobbyInstaller : Installer<LobbyInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainLobbyPresenter>().AsSingle();
        }
    }
}