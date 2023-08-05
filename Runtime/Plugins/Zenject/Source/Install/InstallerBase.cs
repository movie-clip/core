namespace Zenject
{
    public abstract class InstallerBase : IInstaller
    {
        [Inject]
        protected DiContainer Container;
        
        public virtual bool IsEnabled
        {
            get { return true; }
        }

        public abstract void InstallBindings();
    }
}

