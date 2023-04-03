using System;
using Boilerplate.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Boilerplate.EventChannels
{
    #region No Arguments

    public abstract class EventChannel : DescriptionScriptableObject
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            try
            {
                if (OnEventRaised == null)
                    EventUtils.NoListenersError(name);
                else
                    OnEventRaised.Invoke();
            }
            catch (Exception e)
            {
                EventUtils.ErrorBroadcaster(e, name);
            }
        }
    }

    #endregion No Arguments

    #region One Argument

    public abstract class EventChannel<T> : DescriptionScriptableObject
    {
        public UnityAction<T> OnEventRaised;

        public void RaiseEvent(T value)
        {
            try
            {
                if (OnEventRaised == null)
                    EventUtils.NoListenersError(name);
                else
                    OnEventRaised.Invoke(value);
            }
            catch (Exception e)
            {
                EventUtils.ErrorBroadcaster(e, name);
            }
        }
    }

    #endregion One Argument

    #region Two Arguments

    public abstract class EventChannel<T, U> : DescriptionScriptableObject
    {
        public UnityAction<T, U> OnEventRaised;

        public void RaiseEvent(T valueT, U valueU)
        {
            try
            {
                if (OnEventRaised == null)
                    EventUtils.NoListenersError(name);
                else
                    OnEventRaised.Invoke(valueT, valueU);
            }
            catch (Exception e)
            {
                EventUtils.ErrorBroadcaster(e, name);
            }
        }
    }

    #endregion Two Arguments

    #region Three Arguments

    public abstract class EventChannel<T, U, V> : DescriptionScriptableObject
    {
        public UnityAction<T, U, V> OnEventRaised;

        public void RaiseEvent(T valueT, U valueU, V valueV)
        {
            try
            {
                if (OnEventRaised == null)
                    EventUtils.NoListenersError(name);
                else
                    OnEventRaised.Invoke(valueT, valueU, valueV);
            }
            catch (Exception e)
            {
                EventUtils.ErrorBroadcaster(e, name);
            }
        }
    }

    #endregion Three Arguments

    #region Four Arguments

    public abstract class EventChannel<T, U, V, X> : DescriptionScriptableObject
    {
        public UnityAction<T, U, V, X> OnEventRaised;

        public void RaiseEvent(T valueT, U valueU, V valueV, X valueX)
        {
            try
            {
                if (OnEventRaised == null)
                    EventUtils.NoListenersError(name);
                else
                    OnEventRaised.Invoke(valueT, valueU, valueV, valueX);
            }
            catch (Exception e)
            {
                EventUtils.ErrorBroadcaster(e, name);
            }
        }
    }

    #endregion Four Arguments
}