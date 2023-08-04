using System;

namespace Core.UI.Data
{
    public abstract class ViewData : IViewData
    {
        public virtual event Action<IViewData> OnDataChanged = c => { };

        public bool Retained { get; private set; }

        public void Retain()
        {
            Retained = true;
        }

        public void Release()
        {
            Retained = false;
            CallDataChanged();
        }

        public void Dispose()
        {
            OnDataChanged = null;
            OnDispose();
        }

        protected void CallDataChanged()
        {
            OnDataChanged?.Invoke(this);
        }

        protected abstract void OnDispose();
    }

    public interface IViewData : IDisposable
    {
        event Action<IViewData> OnDataChanged;
    }
}