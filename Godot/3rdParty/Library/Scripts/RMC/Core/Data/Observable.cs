namespace RMC.Core.Data.Types
{
    /// <summary>
    /// Delegate for observable events.
    /// </summary>
    public delegate void SuperEventHandler<T, U>(T oldValue, U newValue);

    /// <summary>
    /// The custom event for <see cref="Observable{T}"/>.
    /// </summary>
    public class SuperEvent<T, U>
    {
        //  Fields ----------------------------------------
        private Observable<T> _observable;
        private event SuperEventHandler<T, U> _eventHandler;

        //  Constructor Methods ---------------------------
        public SuperEvent(Observable<T> observable)
        {
            _observable = observable;
        }

        //  Methods ---------------------------------------
        public void AddListener(SuperEventHandler<T, U> call)
        {
            AddListener(call, false);
        }

        /// <summary>
        /// Consuming scope can optionally 'catch up' to the existing
        /// value by immediately refreshing value to the observer.
        /// </summary>
        /// <param name="call"></param>
        /// <param name="willRefreshImmediately"></param>
        public void AddListener(SuperEventHandler<T, U> call, bool willRefreshImmediately)
        {
            _eventHandler += call;
            if (willRefreshImmediately)
            {
                _observable.OnValueChangedRefresh();
            }
        }

        public void RemoveListener(SuperEventHandler<T, U> call)
        {
            _eventHandler -= call;
        }

        public void Invoke(T oldValue, U newValue)
        {
            _eventHandler?.Invoke(oldValue, newValue);
        }
    }

    /// <summary>
    /// Wrapper where any changes to value of type
    /// <see cref="TValue"/> can be observable via events.
    /// </summary>
    public class Observable<TValue>
    {
        //  Events ----------------------------------------
        public readonly SuperEvent<TValue, TValue> OnValueChanged;

        //  Properties ------------------------------------
        /// <summary>
        /// Keep this property name as "Value".
        /// </summary>
        public TValue Value
        {
            set
            {
                _currentValue = OnValueChanging(_currentValue, value);
                OnValueChanged.Invoke(_previousValue, _currentValue);
            }
            get
            {
                return _currentValue;
            }
        }

        //  Fields ----------------------------------------
        private TValue _currentValue;
        private TValue _previousValue;

        //  Constructor Methods ---------------------------
        public Observable()
        {
            OnValueChanged = new SuperEvent<TValue, TValue>(this);
        }

        //  Methods ---------------------------------------
        /// <summary>
        /// Refresh the observers by recalling Invoke()
        /// with the latest values.
        /// </summary>
        public void OnValueChangedRefresh()
        {
            OnValueChanged.Invoke(_previousValue, _currentValue);
        }

        protected virtual TValue OnValueChanging(TValue previousValue, TValue newValue)
        {
            // Optional: Override method to gate/police the value changes
            _previousValue = previousValue;
            return newValue;
        }

        //  Event Handlers --------------------------------
    }
}
