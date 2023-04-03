using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boilerplate.Input
{
    public partial class PlayerInputReader
    {
        #region Variables

        [Foldout("UI Input Events")]
        [SerializeField] private VoidEventChannel _inputUIConfirmEvent;
        [SerializeField] private VoidEventChannel _inputUIBackEvent;
        [SerializeField] private VoidEventChannel _inputUINextEvent;
        [SerializeField] private VoidEventChannel _inputUIPreviousEvent;
        [SerializeField] private VoidEventChannel _inputUIPauseEvent;

        #endregion Variables

        #region UI Callbacks

        public void OnConfirmInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            EventUtils.BroadcastEvent(_inputUIConfirmEvent);
        }

        public void OnBackInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            EventUtils.BroadcastEvent(_inputUIBackEvent);
        }

        public void OnNextInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            EventUtils.BroadcastEvent(_inputUINextEvent);
        }

        public void OnPreviousInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            EventUtils.BroadcastEvent(_inputUIPreviousEvent);
        }

        public void OnUIPauseInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            EventUtils.BroadcastEvent(_inputUIPauseEvent);
        }

        #endregion UI Callbacks
    }
}