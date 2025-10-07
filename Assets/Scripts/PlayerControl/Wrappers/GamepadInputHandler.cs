/* A class to capture Gamepad user input in a centralized way.*/

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public static class GamepadInputHandler
{
    /// <summary>
    /// An Enum to represent all the buttons that can be pressed on our controller.
    /// </summary>
    public enum Button
    {
        DPadUp,
        DPadRight,
        DPadDown,
        DPadLeft,

        ButtonDown, // South (A / Cross)
        ButtonRight, // East (B / Circle)
        ButtonLeft, // West (X / Square)
        ButtonUp, // North (Y / Triangle)

        LeftBumper,
        RightBumper,

        Select,
        Start,

        LeftStick,
        RightStick,
    }

    /// <summary>
    /// An Enum to represent all the axes that can be used on our controller.
    /// The triggers are represented as axes since they can have continuous values.
    /// </summary>
    public enum Axis
    {
        LeftX,
        LeftY,
        RightX,
        RightY,
        LeftTrigger,
        RightTrigger,
    }

    /// <summary>
    /// A class to represent a gamepad.
    /// You have to create a new instance everytime a gamepad is plugged in.
    /// We assign each Gamepad an ID.
    /// </summary>
    public class GamepadController
    {
        private readonly int _index;
        private readonly Gamepad _gamepad;

        public GamepadController(int index)
        {
            _index = index;
            _gamepad = Gamepad.all.Count > index ? Gamepad.all[index] : null;
        }

        public bool IsAvailable()
        {
            return _gamepad != null;
        }

        public bool IsButtonDown(Button button)
        {
            var control = GetButtonControl(button);
            return control != null && control.isPressed;
        }

        public bool IsButtonPressed(Button button)
        {
            var control = GetButtonControl(button);
            return control != null && control.wasPressedThisFrame;
        }

        public bool IsButtonReleased(Button button)
        {
            var control = GetButtonControl(button);
            return control != null && control.wasReleasedThisFrame;
        }

        public float GetAxisMovement(Axis axis)
        {
            if (_gamepad == null)
                return 0f;

            switch (axis)
            {
                case Axis.LeftX:
                    return _gamepad.leftStick.x.ReadValue();
                case Axis.LeftY:
                    return _gamepad.leftStick.y.ReadValue();
                case Axis.RightX:
                    return _gamepad.rightStick.x.ReadValue();
                case Axis.RightY:
                    return _gamepad.rightStick.y.ReadValue();
                case Axis.LeftTrigger:
                    return _gamepad.leftTrigger.ReadValue();
                case Axis.RightTrigger:
                    return _gamepad.rightTrigger.ReadValue();
                default:
                    return 0f;
            }
        }

        private ButtonControl GetButtonControl(Button button)
        {
            if (_gamepad == null)
                return null;

            return button switch
            {
                Button.DPadUp => _gamepad.dpad.up,
                Button.DPadDown => _gamepad.dpad.down,
                Button.DPadLeft => _gamepad.dpad.left,
                Button.DPadRight => _gamepad.dpad.right,

                Button.ButtonDown => _gamepad.buttonSouth,
                Button.ButtonRight => _gamepad.buttonEast,
                Button.ButtonLeft => _gamepad.buttonWest,
                Button.ButtonUp => _gamepad.buttonNorth,

                Button.LeftBumper => _gamepad.leftShoulder,
                Button.RightBumper => _gamepad.rightShoulder,

                Button.Select => _gamepad.selectButton,
                Button.Start => _gamepad.startButton,

                Button.LeftStick => _gamepad.leftStickButton,
                Button.RightStick => _gamepad.rightStickButton,

                _ => null,
            };
        }
    }
}
