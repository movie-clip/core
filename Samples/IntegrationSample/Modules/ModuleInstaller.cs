using Samples.IntegrationSample.Modules.Settings;
using Samples.IntegrationSample.UI.Loading;
using Zenject;

namespace Samples.IntegrationSample.Modules
{
    public class ModuleInstaller : Installer<ModuleInstaller>
    {
        public override void InstallBindings()
        {
            GameSettingsInstaller.Install(Container);
            LobbyInstaller.Install(Container);
        }
    }
}