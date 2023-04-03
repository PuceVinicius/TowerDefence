using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using Boilerplate.InputLayoutCommons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

namespace Boilerplate.InputLayout
{
    public class InputLayout : MonoBehaviour
    {
        [Foldout("References")]
        [SerializeField] private InputLayoutData _data;

        [Foldout("Broadcasters")]
        [SerializeField] private TextureEventChannel _spriteAtlasUpdateEvent;

        public InputLayoutType OnInputLayoutChanged(PlayerInput playerInput)
        {
            var inputLayout = GetCurrentInputLayout(playerInput);

            UpdateSpriteAtlas(inputLayout);
            UpdateCursor(inputLayout);

            return inputLayout;
        }

        private InputLayoutType GetCurrentInputLayout(PlayerInput playerInput)
        {
            switch (playerInput.devices[0])
            {
                case Mouse:
                case Keyboard:
                    return InputLayoutType.KeyboardAndMouse;
                case XInputController:
                    return InputLayoutType.Xbox;
                case DualShockGamepad:
                    return InputLayoutType.Playstation;
                case Touchscreen:
                    return InputLayoutType.Touch;
                default:
                    return InputLayoutType.Xbox;
            }
        }

        private void UpdateSpriteAtlas(InputLayoutType inputLayout)
        {
            _data.SpriteAsset.spriteSheet = inputLayout switch
            {
                InputLayoutType.KeyboardAndMouse => _data.SpriteAtlasPC,
                InputLayoutType.Playstation => _data.SpriteAtlasPS,
                InputLayoutType.Xbox => _data.SpriteAtlasXB,
                _ => _data.SpriteAtlasXB
            };

            EventUtils.BroadcastEvent(_spriteAtlasUpdateEvent, _data.SpriteAsset.spriteSheet);
        }

        private void UpdateCursor(InputLayoutType inputLayout)
        {
            if (inputLayout == InputLayoutType.KeyboardAndMouse)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}