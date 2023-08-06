using Zenject;

namespace Core.UI
{
    public abstract class BasePresenter
    {
        [Inject]
        protected SignalBus SignalBus;

        public abstract void Start();

        public abstract void Finish();
    }
    
    public abstract class BasePresenter<T> : BasePresenter where T : BaseView
    {
        protected abstract string ViewId { get; }
        
        protected T View;

        public override void Start()
        {
            View =  ViewManager.Instance.SetView(ViewId) as T;
            OnStart();
        }
        
        public override void Finish()
        {
            ViewManager.Instance.RemoveView(View);
            OnFinish();
        }
        
        protected virtual void OnStart()
        {
        }

        protected virtual void OnFinish()
        {
        }
    }
}