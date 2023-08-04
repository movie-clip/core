using Core.Installers;
using Core.Network.Data;

namespace Core.Services.Network
{
    public class NetworkInstaller : BaseInstaller
    {
        protected override void InstallServices()
        {
            base.InstallServices();
            
            Container.BindInterfacesAndSelfTo<NetworkService>().AsSingle();
        }

        protected override void InstallDataProviders()
        {
            base.InstallDataProviders();
            
            Container.BindInterfacesAndSelfTo<NetworkModel>().AsSingle();
        }
    }
}