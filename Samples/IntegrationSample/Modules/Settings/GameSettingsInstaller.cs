using Samples.IntegrationSample.Actions;
using Zenject;

namespace Samples.IntegrationSample.Modules.Settings
{
    public class GameSettingsInstaller : Installer<GameSettingsInstaller>
    {
        [Inject] private SignalBus _signalBus;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSettingsModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameSettingsService>().AsSingle();

            Container.BindInterfacesAndSelfTo<GetAppSettingsAction>().AsSingle();

            _signalBus.DeclareSignal<GameSettingsSignal.Updated>();
        }
    }
}