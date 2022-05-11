using System;

namespace TextProject.Utils
{
    public class SubscriptionProperty<T> : ISubscriptionProperty<T>
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onValueChanged.Invoke(_value);
            }
        }

        private Action<T> _onValueChanged = obj => { };

        public void SubscribeOnChange(Action<T> subscriptionAction)
        {
            _onValueChanged += subscriptionAction;
        }

        public void UnsubscribeFromChange(Action<T> subscriptionAction)
        {
            _onValueChanged -= subscriptionAction;
        }
    }
}