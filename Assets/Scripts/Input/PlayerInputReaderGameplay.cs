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

        #endregion Variables

        #region Gameplay Callbacks

        public void OnGameplayPauseInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            EventUtils.BroadcastEvent(_inputGameplayPauseEvent);
        }

        #endregion Gameplay Callbacks
    }
}