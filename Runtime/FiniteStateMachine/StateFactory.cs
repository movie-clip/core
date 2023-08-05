using Zenject;

namespace Core
{
    public class StateFactory
    {
        [Inject]
        private DiContainer diContainer;

        public T Create<T>()
        {
            return diContainer.Instantiate<T>();
        }
    }
}