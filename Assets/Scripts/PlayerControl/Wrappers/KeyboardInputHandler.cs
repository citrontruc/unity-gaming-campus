/*A class to handle keyboard controls. */

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public static class KeyboardInputHandler
{
    /// <summary>
    /// An enum to represent the keys that we are using in our game.
    /// </summary>
    public enum Key
    {
        Up,
        Down,
        Left,
        Right,

        Space,
        Enter,
    }

    public class KeyboardDevice
    {
        private readonly Keyboard _keyboard;

        public KeyboardDevice()
        {
            _keyboard = Keyboard.current;
        }

        public bool IsAvailable()
        {
            return _keyboard != null;
        }

        public bool IsKeyDown(Key key)
        {
            var control = GetKeyControl(key);
            return control != null && control.isPressed;
        }

        public bool IsKeyPressed(Key key)
        {
            var control = GetKeyControl(key);
            return control != null && control.wasPressedThisFrame;
        }

        public bool IsKeyReleased(Key key)
        {
            var control = GetKeyControl(key);
            return control != null && control.wasReleasedThisFrame;
        }

        private KeyControl GetKeyControl(Key key)
        {
            if (_keyboard == null)
                return null;

            return key switch
            {
                Key.Up => _keyboard.upArrowKey,
                Key.Down => _keyboard.downArrowKey,
                Key.Left => _keyboard.leftArrowKey,
                Key.Right => _keyboard.rightArrowKey,

                Key.Space => _keyboard.spaceKey,
                Key.Enter => _keyboard.enterKey,

                _ => null,
            };
        }
    }
}
