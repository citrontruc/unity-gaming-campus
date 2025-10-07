using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public static class MouseInputHandler
{
    public enum Button
    {
        Left,
        Right,
        Middle,
        Forward,
        Back
    }

    public enum Axis
    {
        X,
        Y,
        Scroll
    }

    public class MouseDevice
    {
        private readonly Mouse _mouse;

        public MouseDevice()
        {
            _mouse = Mouse.current;
        }

        public bool IsAvailable()
        {
            return _mouse != null;
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

        public Vector2 GetAxisMovement(Axis axis)
        {
            if (_mouse == null) return Vector2.zero;

            switch (axis)
            {
                case Axis.X: return new Vector2(_mouse.delta.x.ReadValue(), 0);
                case Axis.Y: return new Vector2(0, _mouse.delta.y.ReadValue());
                case Axis.Scroll: return _mouse.scroll.ReadValue();
                default: return Vector2.zero;
            }
        }

        public Vector2 GetPosition()
        {
            return _mouse?.position.ReadValue() ?? Vector2.zero;
        }

        public Vector2 GetDelta()
        {
            return _mouse?.delta.ReadValue() ?? Vector2.zero;
        }

        private ButtonControl GetButtonControl(Button button)
        {
            if (_mouse == null) return null;

            return button switch
            {
                Button.Left => _mouse.leftButton,
                Button.Right => _mouse.rightButton,
                Button.Middle => _mouse.middleButton,
                Button.Forward => _mouse.forwardButton,
                Button.Back => _mouse.backButton,
                _ => null
            };
        }
    }
}
