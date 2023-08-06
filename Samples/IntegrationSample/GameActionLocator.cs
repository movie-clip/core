using Samples.IntegrationSample.Actions;
using Zenject;

namespace Samples.IntegrationSample
{
    public class GameActionLocator
    {
        private static GameActionLocator _instance;
        public static GameActionLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    return new GameActionLocator();
                }

                return _instance;
            }
        }
        public GameActionLocator()
        {
            _instance = this;
        }
        
        [Inject]
        private GetAppSettingsAction getAppSettingsAction;
        
        public static GetAppSettingsAction GetAppSettingsAction => Instance.getAppSettingsAction;
    }
}