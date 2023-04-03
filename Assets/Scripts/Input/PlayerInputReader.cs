using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using Boilerplate.InputCommons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boilerplate.Input
{
    public partial class PlayerInputReader : MonoBehaviour
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private PlayerInput _playerInput;

        [Foldout("Broadcasters")]
        [SerializeField] private PlayerInputEventChannel _onControlsChangedEvent;
        [SerializeField] private VoidEventChannel _inputAnyEvent;

        [Foldout("Listeners")]
        [SerializeField] private BoolEventChannel _onToggleInputsEvent;
        [SerializeField] private VoidEventChannel _setUIActionMapEvent;
        [SerializeField] private VoidEventChannel _setGameplayActionMapEvent;

        #endregion Variables

        #region Messages

        private void OnEnable()
        {
            EventUtils.AddEventListener(_onToggleInputsEvent, OnToggleInputs);
            EventUtils.AddEventListener(_setUIActionMapEvent, SetUIActionMap);
            EventUtils.AddEventListener(_setGameplayActionMapEvent, SetGameplayActionMap);
        }

        private void OnDisable()
        {
            EventUtils.RemoveEventListener(_onToggleInputsEvent, OnToggleInputs);
            EventUtils.RemoveEventListener(_setUIActionMapEvent, SetUIActionMap);
            EventUtils.RemoveEventListener(_setGameplayActionMapEvent, SetGameplayActionMap);
        }

        #endregion Messages

        #region Methods

        private void SetUIActionMap() => SwitchActionMap(PlayerInputConsts.ACTION_MAP_UI);
        private void SetGameplayActionMap() => SwitchActionMap(PlayerInputConsts.ACTION_MAP_GAMEPLAY);
        private void SwitchActionMap(string actionMap) => _playerInput.SwitchCurrentActionMap(actionMap);

        private void OnToggleInputs(bool value)
        {
            if (value)
                _playerInput.ActivateInput();
            else
                _playerInput.DeactivateInput();
        }

        #endregion Methods

        #region Input Callbacks

        public void OnControlsChanged()
        {
            if (!_playerInput)
                return;

            EventUtils.BroadcastEvent(_onControlsChangedEvent, _playerInput);
        }

        public void OnAnyInput(InputAction.CallbackContext context) => EventUtils.BroadcastEvent(_inputAnyEvent);

        #endregion Input Callbacks
    }
}