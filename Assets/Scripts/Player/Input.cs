/*
A Script to handle player input. It uses Unity's input system.
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class Input : Singleton<Input>
{
    #region Event Channels
    [Header("Event Channels")]
    [SerializeField]
    private MoveEventChannelSO _moveEventChannel;

    [SerializeField]
    private JumpEventChannelSO _jumpEventChannel;

    [SerializeField]
    private SpecialEventChannelSO _specialEventChannel;

    [SerializeField]
    private DashEventChannelSO _dashEventChannel;
    #endregion
    /// <summary>
    /// Reference to the actions our player will take.
    /// </summary>
    #region Actions
    [Header("Movement")]
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _specialAction;
    private InputAction _dashAction;
    #endregion

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _specialAction = InputSystem.actions.FindAction("Special");
        _dashAction = InputSystem.actions.FindAction("Dash");
    }

    private void Update()
    {
        // Movement
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        _moveEventChannel.RaiseEvent(moveValue);

        // Dash
        _jumpEventChannel.RaiseEvent(_jumpAction.IsPressed());

        // Special
        _specialEventChannel.RaiseEvent(_specialAction.IsPressed());

        // Dash
        _dashEventChannel.RaiseEvent(_dashAction.IsPressed());
    }
}
