using System;
using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using Boilerplate.FuncChannels;
using Boilerplate.Utilities;
using UnityEngine;

namespace TowerDefence.CameraManager
{
    public class CameraManager : MonoBehaviour
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private Camera _camera;

        [Foldout("Delegates")]
        [SerializeField] private CameraVoidFuncChannel _setCameraFunc;

        #endregion

        #region Methods

        private void OnEnable() => FuncUtils.SetDelegate(_setCameraFunc, SetCamera);

        private void OnDisable() => FuncUtils.ResetDelegate(_setCameraFunc);

        private Camera SetCamera() => _camera;

        #endregion
    }
}