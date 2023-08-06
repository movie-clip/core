using Core;
using Core.UI;
using Core.UI.Data;
using Samples.IntegrationSample.AppState;
using Samples.IntegrationSample.Modules;
using Samples.IntegrationSample.UI;
using Zenject;

namespace Samples.IntegrationSample
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallViews();
            
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
            
            ModuleInstaller.Install(Container);
            Container.Inject(GameActionLocator.Instance);
            
            Container.BindInterfacesAndSelfTo<AppReadyPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<AppStateChart>().AsSingle();
        }

        private void InstallViews()
        {
            ViewManager.Instance.Initialize();
            ViewManager.Instance.RegisterView(ViewNames.LoadingView, LayerNames.ScreenLayer);
            ViewManager.Instance.RegisterView(ViewNames.MainLobby, LayerNames.ScreenLayer);
        }
    }
}