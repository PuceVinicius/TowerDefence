using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boilerplate.Utilities
{
    public class GlobalUtilities : MonoBehaviour
    {
        #region Events

        public static event Action UpdateEvent;

        #endregion

        #region Messages

        private void Update()
        {
            UnityEngine.Profiling.Profiler.BeginSample("Global Update");

            UpdateEvent?.Invoke();

            UnityEngine.Profiling.Profiler.EndSample();
        }

        #endregion Messages
    }
}