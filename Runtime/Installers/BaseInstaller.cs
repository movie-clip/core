using Zenject;

namespace Core.Installers
{
    public abstract class BaseInstaller : InstallerBase
    {
        public override void InstallBindings()
        {
            InstallServices();
            InstallDataProviders();
            InstallActionProviders();
        }
        
        protected virtual void InstallServices()
        {
        }

        protected virtual void InstallDataProviders()
        {
        }

        protected virtual void InstallActionProviders()
        {
        }
    }
}