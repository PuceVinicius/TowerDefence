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

        [Foldout("Gameplay Input Events")]
        [SerializeField] private VoidEventChannel _inputGameplayPauseEvent;
        [SerializeField] private VoidEventChannel _inputStartPlaceTowerEvent;
        [SerializeField] private VoidEventChannel _inputEndPlaceTowerEvent;

        #endregion Variables

        #region Gameplay Callbacks

        public void OnGameplayPauseInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            EventUtils.BroadcastEvent(_inputGameplayPauseEvent);
        }

        public void OnPlaceTowerInput(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    EventUtils.BroadcastEvent(_inputStartPlaceTowerEvent);
                    break;
                case InputActionPhase.Canceled:
                    EventUtils.BroadcastEvent(_inputEndPlaceTowerEvent);
                    break;
            }

        }

        #endregion Gameplay Callbacks
    }
}