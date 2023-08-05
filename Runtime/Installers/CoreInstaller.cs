using Core.Actions;
using Core.Data;
using Core.Data.Time;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallServices();
            InstallData();
            InstallAction();
            InstallSignals();

            Container.Inject(CoreDataProvider.Instance);
            Container.Inject(CoreActionLocator.Instance);
        }

        private void InstallServices()
        {
        }

        private void InstallData()
        {
            Container.BindInterfacesAndSelfTo<DateTimeProvider>().AsSingle();
        }

        private void InstallAction()
        {
            Container.BindInterfacesAndSelfTo<CoreDataProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<CoreActionLocator>().AsSingle();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}
