using System;

namespace App
{
    public class ObservableVariable<T>
    {
        public event Action<T> OnBeforeUpdate;
        public event Action<T> OnAfterUpdate;

        private T _value = default;

        public T Value
        {
            get => _value;
            set
            {
                OnBeforeUpdate?.Invoke(_value);
                _value = value;
                OnAfterUpdate?.Invoke(_value);
            }
        }
    }
}