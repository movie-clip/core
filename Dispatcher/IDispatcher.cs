using System;

namespace Core.Dispatcher
{
    public interface IDispatcher
    {

        void AddListener(string eventId, Action callback);

        void RemoveListener(string eventId, Action callback);

        void Dispatch(string eventId);
    }
}
