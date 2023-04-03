using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Boilerplate.EventChannels
{
    public static class EventUtils
    {
        #region Consts

        public const string EVENT_ASSET_MENU_BASE_NAME_PREFIX = "ScriptableObjects/Channels/Event/";

#if UNITY_EDITOR
        private const bool SHOW_DEBUG_LOG = false;
        private const bool SHOW_DEBUG_ERROR = true;
#else
        private const bool SHOW_DEBUG_LOG = false;
        private const bool SHOW_DEBUG_ERROR = true;
#endif

        #endregion Consts

        #region Public Methods

        #region | No Arguments

        public static bool AddEventListener(EventChannel eventChannel, UnityAction method) => GenericEventListener(eventChannel, method, true);
        public static bool RemoveEventListener(EventChannel eventChannel, UnityAction method) => GenericEventListener(eventChannel, method, false);
        public static bool BroadcastEvent(EventChannel eventChannel) => EventBroadcaster(eventChannel);

        #endregion | No Arguments

        #region | One Argument

        public static bool AddEventListener<T>(EventChannel<T> eventChannel, UnityAction<T> method) => GenericEventListener(eventChannel, method, true);
        public static bool RemoveEventListener<T>(EventChannel<T> eventChannel, UnityAction<T> method) => GenericEventListener(eventChannel, method, false);
        public static bool BroadcastEvent<T>(EventChannel<T> eventChannel, T valueT) => EventBroadcaster(eventChannel, valueT);

        #endregion | One Argument

        #region | Two Arguments

        public static bool AddEventListener<T, U>(EventChannel<T, U> eventChannel, UnityAction<T, U> method) => GenericEventListener(eventChannel, method, true);
        public static bool RemoveEventListener<T, U>(EventChannel<T, U> eventChannel, UnityAction<T, U> method) => GenericEventListener(eventChannel, method, false);
        public static bool BroadcastEvent<T, U>(EventChannel<T, U> eventChannel, T valueT, U valueU) => EventBroadcaster(eventChannel, valueT, valueU);

        #endregion | Two Arguments

        #region | Three Arguments

        public static bool AddEventListener<T, U, V>(EventChannel<T, U, V> eventChannel, UnityAction<T, U, V> method) => GenericEventListener(eventChannel, method, true);
        public static bool RemoveEventListener<T, U, V>(EventChannel<T, U, V> eventChannel, UnityAction<T, U, V> method) => GenericEventListener(eventChannel, method, false);
        public static bool BroadcastEvent<T, U, V>(EventChannel<T, U, V> eventChannel, T valueT, U valueU, V valueV) => EventBroadcaster(eventChannel, valueT, valueU, valueV);

        #endregion | Three Arguments

        #region | Four Arguments

        public static bool AddEventListener<T, U, V, X>(EventChannel<T, U, V, X> eventChannel, UnityAction<T, U, V, X> method) => GenericEventListener(eventChannel, method, true);
        public static bool RemoveEventListener<T, U, V, X>(EventChannel<T, U, V, X> eventChannel, UnityAction<T, U, V, X> method) => GenericEventListener(eventChannel, method, false);
        public static bool BroadcastEvent<T, U, V, X>(EventChannel<T, U, V, X> eventChannel, T valueT, U valueU, V valueV, X valueX) => EventBroadcaster(eventChannel, valueT, valueU, valueV, valueX);

        #endregion | Four Arguments

        #region | Error Handler

        public static void NoListenersError(string name)
        {
            if (SHOW_DEBUG_LOG)
                Debug.Log($"[EventChannels] No listeners to the EventChannel {name}! {ErrorHandler(null)}");
        }

        #endregion | Error Handler

        #endregion Public Methods

        #region Auxiliary Methods

        #region | Error Handler

        private static void ErrorListener(Exception e, bool isAdd, string name)
        {
            if (SHOW_DEBUG_ERROR)
                Debug.LogError(
                    $"[EventChannels] Failed to {(isAdd ? "add" : "remove")} listener for the EventChannel {name}. {ErrorHandler(e)}"
                );
        }

        public static void ErrorBroadcaster(Exception e, string name)
        {
            if (SHOW_DEBUG_ERROR)
                Debug.LogError($"[EventChannels] Error broadcasting the EventChannel {name}! {ErrorHandler(e)}");
        }

        private static string ErrorHandler(Exception e) =>
            e != null ? $" \n {e}" : "";

        #endregion | Error Handler

        #region | Broadcasters

        private static bool EventBroadcaster(EventChannel eventChannel)
        {
            try
            {
                eventChannel.RaiseEvent();
            }
            catch (Exception e)
            {
                ErrorBroadcaster(e, eventChannel.name);
                return false;
            }
            return true;
        }

        private static bool EventBroadcaster<T>(EventChannel<T> eventChannel, T valueT)
        {
            try
            {
                eventChannel.RaiseEvent(valueT);
            }
            catch (Exception e)
            {
                ErrorBroadcaster(e, eventChannel.name);
                return false;
            }
            return true;
        }

        private static bool EventBroadcaster<T, U>(EventChannel<T, U> eventChannel, T valueT, U valueU)
        {
            try
            {
                eventChannel.RaiseEvent(valueT, valueU);
            }
            catch (Exception e)
            {
                ErrorBroadcaster(e, eventChannel.name);
                return false;
            }
            return true;
        }

        private static bool EventBroadcaster<T, U, V>(EventChannel<T, U, V> eventChannel, T valueT, U valueU, V valueV)
        {
            try
            {
                eventChannel.RaiseEvent(valueT, valueU, valueV);
            }
            catch (Exception e)
            {
                ErrorBroadcaster(e, eventChannel.name);
                return false;
            }
            return true;
        }

        private static bool EventBroadcaster<T, U, V, X>(EventChannel<T, U, V, X> eventChannel, T valueT, U valueU, V valueV,
            X valueX)
        {
            try
            {
                eventChannel.RaiseEvent(valueT, valueU, valueV, valueX);
            }
            catch (Exception e)
            {
                ErrorBroadcaster(e, eventChannel.name);
                return false;
            }
            return true;
        }

        #endregion | Broadcasters

        #region | Listeners

        private static bool GenericEventListener(EventChannel eventChannel, UnityAction method, bool isAdd)
        {
            try
            {
                if (isAdd)
                    eventChannel.OnEventRaised += method;
                else
                    eventChannel.OnEventRaised -= method;
            }
            catch (Exception e)
            {
                ErrorListener(e, isAdd, eventChannel.name);
                return false;
            }

            return true;
        }

        private static bool GenericEventListener<T>(EventChannel<T> eventChannel, UnityAction<T> method, bool isAdd)
        {
            try
            {
                if (isAdd)
                    eventChannel.OnEventRaised += method;
                else
                    eventChannel.OnEventRaised -= method;
            }
            catch (Exception e)
            {
                ErrorListener(e, isAdd, eventChannel.name);
                return false;
            }

            return true;
        }

        private static bool GenericEventListener<T, U>(EventChannel<T, U> eventChannel, UnityAction<T, U> method, bool isAdd)
        {
            try
            {
                if (isAdd)
                    eventChannel.OnEventRaised += method;
                else
                    eventChannel.OnEventRaised -= method;
            }
            catch (Exception e)
            {
                ErrorListener(e, isAdd, eventChannel.name);
                return false;
            }

            return true;
        }

        private static bool GenericEventListener<T, U, V>(EventChannel<T, U, V> eventChannel, UnityAction<T, U, V> method, bool isAdd)
        {
            try
            {
                if (isAdd)
                    eventChannel.OnEventRaised += method;
                else
                    eventChannel.OnEventRaised -= method;
            }
            catch (Exception e)
            {
                ErrorListener(e, isAdd, eventChannel.name);
                return false;
            }

            return true;
        }

        private static bool GenericEventListener<T, U, V, X>(EventChannel<T, U, V, X> eventChannel, UnityAction<T, U, V, X> method, bool isAdd)
        {
            try
            {
                if (isAdd)
                    eventChannel.OnEventRaised += method;
                else
                    eventChannel.OnEventRaised -= method;
            }
            catch (Exception e)
            {
                ErrorListener(e, isAdd, eventChannel.name);
                return false;
            }

            return true;
        }

        #endregion | Listeners

        #endregion Auxiliary Methods
    }
}