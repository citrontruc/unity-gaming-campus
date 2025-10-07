/*A class to handle the plugging of gamepads and getting the inputs of the gamepad.*/

using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadManager : MonoBehaviour
{
    private GamepadInputHandler.GamepadController currentPad;

    public GamepadManager()
    {
        ServiceLocator.Register<GamepadManager>(this);
    }

    void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        RefreshGamepad();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad)
            RefreshGamepad();
    }

    /// <summary>
    /// Check if there are any gamepads and chooses the first one if there are multiple.
    /// Assigns it the index 0.
    /// </summary>
    private void RefreshGamepad()
    {
        if (Gamepad.all.Count > 0)
            currentPad = new GamepadInputHandler.GamepadController(0);
        else
            currentPad = null;
    }

    void Update()
    {
        if (currentPad == null || !currentPad.IsAvailable())
            return;

        if (currentPad.IsButtonPressed(GamepadInputHandler.Button.ButtonDown))
            Debug.Log("Button Pressed");
    }
}
