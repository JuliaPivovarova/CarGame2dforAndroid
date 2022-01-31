using System;

namespace Code.Interfaces
{
    public interface IReadOlnySubscribeProperty<T>
    {
        T Value { get; }
        void SubscribeOnChange(Action<T> subscribeAction);
        void UnSubscribeOnChange(Action<T> unSubscribeAction);
    }
}