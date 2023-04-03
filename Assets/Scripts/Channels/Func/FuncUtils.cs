using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Boilerplate.FuncChannels
{
    public class FuncUtils
    {
        #region Consts

        private const string GENERIC_PREFIX = "The FuncChannel of type {0} is 'null'!";
        private const string GENERIC_PREFIX_WITH_NAME = "The FuncChannel {0} of type {1} has error";
        private const string GENERIC_ERROR_DELEGATE_NULL = ": delegate method is null!";
        private const string GENERIC_ERROR_CALLING_METHOD = " calling the delegate method!";

        private const int FUNC_UTILS_SKIP_FRAMES = 3;

        public const string FUNC_ASSET_MENU_BASE_NAME_PREFIX = "ScriptableObjects/Channels/Func/";

        #endregion

        #region Public Methods

        #region | No Arguments

        public static void SetDelegate<R>(FuncChannel<R> func, Func<R> delegateMethod)
        {
            try
            {
                func.FuncDelegate = delegateMethod;
            }
            catch
            {
                FuncError(func, delegateMethod);
            }
        }

        public static void ResetDelegate<R>(FuncChannel<R> func)
        {
            try
            {
                func.FuncDelegate = null;
            }
            catch
            {
                FuncError(func);
            }
        }

        public static R CallDelegate<R>(FuncChannel<R> func)
        {
            try
            {
                return func.FuncDelegate();
            }
            catch
            {
                if (func)
                    FuncError(func, func.FuncDelegate);
                else
                    FuncError(func);
                return default;
            }
        }

        #endregion | No Arguments

        #region | One Argument

        public static void SetDelegate<R, A>(FuncChannel<R, A> func, Func<A, R> delegateMethod)
        {
            try
            {
                func.FuncDelegate = delegateMethod;
            }
            catch
            {
                FuncError(func, delegateMethod);
            }
        }

        public static void ResetDelegate<R, A>(FuncChannel<R, A> func)
        {
            try
            {
                func.FuncDelegate = null;
            }
            catch
            {
                FuncError(func);
            }
        }

        public static R CallDelegate<R, A>(FuncChannel<R, A> func, A arg)
        {
            try
            {
                return func.FuncDelegate(arg);
            }
            catch
            {
                if (func)
                    FuncError(func, func.FuncDelegate);
                else
                    FuncError(func);
                return default;
            }
        }

        #endregion | One Argument

        #endregion Public Methods

        #region Error Handler

        private static void FuncError<R, A>(FuncChannel<R, A> func, Func<A, R> delegateMethod)
        {
#if UNITY_EDITOR
            var message = "";
            if (!func)
            {
                message = string.Format(GENERIC_PREFIX, typeof(R));
                LogFuncException(message, FUNC_UTILS_SKIP_FRAMES);
                return;
            }

            message = string.Format(GENERIC_PREFIX_WITH_NAME, func.name, typeof(R));

            message += delegateMethod == null ? GENERIC_ERROR_DELEGATE_NULL : GENERIC_ERROR_CALLING_METHOD;

            LogFuncException(message, FUNC_UTILS_SKIP_FRAMES);
#endif
        }

        private static void FuncError<R>(FuncChannel<R> func, Func<R> delegateMethod)
        {
#if UNITY_EDITOR
            var message = "";
            if (!func)
            {
                message = string.Format(GENERIC_PREFIX, typeof(R));
                LogFuncException(message, FUNC_UTILS_SKIP_FRAMES);
                return;
            }

            message = string.Format(GENERIC_PREFIX_WITH_NAME, func.name, typeof(R));

            message += delegateMethod == null ? GENERIC_ERROR_DELEGATE_NULL : GENERIC_ERROR_CALLING_METHOD;

            LogFuncException(message, FUNC_UTILS_SKIP_FRAMES);
#endif
        }

        private static void FuncError<R, A>(FuncChannel<R, A> func)
        {
#if UNITY_EDITOR
            var message = "";
            if (!func)
                message += string.Format(GENERIC_PREFIX, typeof(R));

            LogFuncException(message, FUNC_UTILS_SKIP_FRAMES);
#endif
        }

        private static void FuncError<R>(FuncChannel<R> func)
        {
#if UNITY_EDITOR
            var message = "";
            if (!func)
                message += string.Format(GENERIC_PREFIX, typeof(R));

            LogFuncException(message, FUNC_UTILS_SKIP_FRAMES);
#endif
        }

        private static void LogFuncException(string message, int skipFrames)
        {
#if UNITY_EDITOR
            var stackTrace = new StackTrace(skipFrames, true);
            ExceptionUtilities.SetStackTrace(out var ex, stackTrace, message);

            Debug.LogException(ex);
#endif
        }

        #endregion Error Handler
    }

    #region Helpers

#if UNITY_EDITOR
    //This class exists for one reason: remove the FuncUtils messages on console's stacktrace if an error is caught (FuncUtils on console was uninformative).
    //Doing some research, I found out that on Unity 2022 versions, a native attribute was added to do exactly it. Link:
    //https://docs.unity3d.com/2022.2/Documentation/ScriptReference/HideInCallstackAttribute.html
    public static class ExceptionUtilities
    {
        private static readonly FieldInfo STACK_TRACE_STRING_FI = typeof(Exception).GetField("_stackTraceString", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly Type TRACE_FORMAT_TI = Type.GetType("System.Diagnostics.StackTrace").GetNestedType("TraceFormat", BindingFlags.NonPublic);
        private static readonly MethodInfo TRACE_TO_STRING_MI = typeof(StackTrace).GetMethod("ToString", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { TRACE_FORMAT_TI }, null);

        public static void SetStackTrace(out Exception target, StackTrace stack, string message)
        {
            target = new Exception(message);
            var getStackTraceString = TRACE_TO_STRING_MI.Invoke(stack, new object[] { Enum.GetValues(TRACE_FORMAT_TI).GetValue(0) });
            STACK_TRACE_STRING_FI.SetValue(target, getStackTraceString);
        }
    }
#endif

    #endregion
}