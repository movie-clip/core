
namespace Core.Actions
{
    public class CoreActionLocator
    {
        private static CoreActionLocator _instance;
        
        public static CoreActionLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    return new CoreActionLocator();
                }

                return _instance;
            }
        }

        public CoreActionLocator()
        {
            _instance = this;
        }
    }
}