using Core.Network.Data;
using Zenject;

namespace Core.Data
{
    public class CoreDataProvider
    {
        private static CoreDataProvider _instance;
        
        public static CoreDataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    return new CoreDataProvider();
                }

                return _instance;
            }
        }

        public CoreDataProvider()
        {
            _instance = this;
        }
        
        [Inject]
        private INetworkDataProvider _networkDataProvider;

        public static INetworkDataProvider NetworkDataProvider => Instance._networkDataProvider;
    }
}