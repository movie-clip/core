using Core.UI.Data;
using Zenject;

namespace Core.UI
{
    public class BaseView : MonoScheduledBehaviour
    {
        public ViewLayer Layer { get; set; }

        [Inject]
        public SignalBus MessageHub { get; set; }
    }

    public class BaseView<T> : BaseView where T : IViewData
    {
        public T Data;

        public void SetContext(T context)
        {
            if (Data != null)
            {
                Data.OnDataChanged -= OnDataChanged;
                Data.Dispose();
            }
            
            Data = context;
            if (Data != null)
            {
                Data.OnDataChanged += OnDataChanged;
            }
            
            OnContextUpdate(Data);
        }

        public void RefreshView()
        {
            OnContextUpdate(Data);
        }
        
        private void OnDataChanged(IViewData viewData)
        {
            OnContextUpdate((T) viewData);
        }

        protected virtual void OnContextUpdate(T context) { }

        protected override void OnReleaseResources()
        {
            if (Data != null)
            {
                Data.OnDataChanged -= OnDataChanged;
                Data.Dispose();
            }

            base.OnReleaseResources();
        }
    }
}