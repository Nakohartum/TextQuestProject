using System;

namespace TextProject.Utils
{
    public interface ISubscriptionProperty<out T>
    {
        T Value { get; }
        void SubscribeOnChange(Action<T> subscriptionAction);
        void UnsubscribeFromChange(Action<T> subscriptionAction);
    }
}