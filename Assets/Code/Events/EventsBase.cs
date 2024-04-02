using System;

namespace GlobalEventSystem
{

    public static class Events
    {
        #region VARIABLES

        private static UIEvents _ui;
        private static GameplayEvents _gameplay;
        private static ModifiableValueEvents _modValue;

        #endregion

        #region PROPERTIES

        public static UIEvents UI
        {
            get
            {
                if (_ui == null)
                    _ui = new UIEvents();
                return _ui;
            }
        }

        public static GameplayEvents Gameplay
        {
            get
            {
                if (_gameplay == null)
                    _gameplay = new GameplayEvents();
                return _gameplay;
            }
        }

        public static ModifiableValueEvents ModValue
        {
            get
            {
                if (_modValue == null)
                    _modValue = new ModifiableValueEvents();
                return _modValue;
            }
        }


        #endregion

        #region CLASSES

        public abstract class EventBase
        {
            #region VARIABLES

            private event Action _event;

            #endregion

            #region CONSTRUCTORS

            public EventBase() { }

            #endregion

            #region METHODS

            public void Subscribe(Action subscriber)
            {
                _event += subscriber;
            }
            public void Unsubscribe(Action subscriber)
            {
                _event -= subscriber;
            }
            public static EventBase operator +(EventBase e, Action subscriber)
            {
                e.Subscribe(subscriber);
                return e;
            }
            public static EventBase operator -(EventBase e, Action subscriber)
            {
                e.Unsubscribe(subscriber);
                return e;
            }

            protected void InvokeBase()
            {
                if (_event != null)
                    _event.Invoke();
            }

            #endregion
        }

        public class Event : EventBase
        {
            #region CONSTRUCTORS

            public Event() : base() { }

            #endregion

            #region VARIABLES

            public void Invoke()
            {
                InvokeBase();
            }
            public static Event operator +(Event e, Action subscriber)
            {
                e.Subscribe(subscriber);
                return e;
            }
            public static Event operator -(Event e, Action subscriber)
            {
                e.Unsubscribe(subscriber);
                return e;
            }

            #endregion
        }
        public class Event<T> : EventBase
        {
            #region VARIABLES

            private event Action<T> _event;

            #endregion

            #region CONSTRUCTORS

            public Event() : base()
            {
            }

            #endregion

            #region METHODS

            public void Subscribe(Action<T> subscriber)
            {
                _event += subscriber;
            }
            public void Unsubscribe(Action<T> subscriber)
            {
                _event -= subscriber;
            }

            public static Event<T> operator +(Event<T> e, Action<T> subscriber)
            {
                e.Subscribe(subscriber);
                return e;
            }
            public static Event<T> operator -(Event<T> e, Action<T> subscriber)
            {
                e.Unsubscribe(subscriber);
                return e;
            }

            public void Invoke(T args)
            {
                if (_event != null)
                    _event.Invoke(args);

                InvokeBase();
            }

            #endregion
        }

        public class Event<T1, T2> : EventBase
        { 
            #region VARIABLES

            private event Action<T1, T2> _event;

            #endregion

            #region CONSTRUCTORS

            public Event() : base()
            {
            }

            #endregion

            #region METHODS

            public void Subscribe(Action<T1, T2> subscriber)
            {
                _event += subscriber;
            }
            public void Unsubscribe(Action<T1, T2> subscriber)
            {
                _event -= subscriber;
            }

            public static Event<T1, T2> operator +(Event<T1, T2> e, Action<T1, T2> subscriber)
            {
                e.Subscribe(subscriber);
                return e;
            }
            public static Event<T1, T2> operator -(Event<T1, T2> e, Action<T1, T2> subscriber)
            {
                e.Unsubscribe(subscriber);
                return e;
            }

            public void Invoke(T1 args1, T2 args2)
            {
                if (_event != null)
                    _event.Invoke(args1, args2);

                InvokeBase();
            }

            #endregion
        }
        #endregion
    }
}