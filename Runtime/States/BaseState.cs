using Core.UI;
using Zenject;

namespace Core.States
{
    public abstract class BaseState
    {
        
        protected DiContainer container;

        [Inject]
        public BaseState(DiContainer container)
        {
            this.container = container;
        }
        
        public void Start()
        {
            OnStart();
        }
        
        protected virtual void OnStart()
        {
        }
        
        public void Finish()
        {
            OnFinish();
        }
        
        protected virtual void OnFinish()
        {
        }
    }

    public abstract class BaseState<T> : BaseState where T : BasePresenter
    {
        protected T presenter;

        protected BaseState(DiContainer container) : base(container)
        {
        }
        
        protected override void OnStart()
        {
            base.OnStart();
            
            presenter = container.Instantiate<T>();
            presenter.Start();
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            
            presenter.Finish();
        }
    }
}