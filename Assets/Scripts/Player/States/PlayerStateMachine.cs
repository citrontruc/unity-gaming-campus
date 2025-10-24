/*
A state machine to handle player state transition.
Also handles special powers.
*/

using System;
using TMPro;
using UnityEngine;

[Serializable]
public class PlayerStateMachine : MonoBehaviour
{
    #region State change
    public enum PlayerState
    {
        EggState,
        ChickState,
        ChickenState,
        RoosterState,
        SuperRoosterState,
        DinosaurState,
    }

    [SerializeField]
    private StateChangeEventChannelSO _stateChangeChannelEvent;
    [SerializeField]
    private PlayerPowerUp _playerPowerUp;
    public IState CurrentState { get; private set; }
    #endregion

    /// <summary>
    /// Special powers
    /// </summary>
    private int _specialCharge = 1;
    public TMP_Text SpecialText;

    #region Monobehaviour methods
    public void Awake()
    {
        CurrentState = new EggState();
    }

    public void Update()
    {
        if (SpecialText != null)
        {
            SpecialText.text = $"Special: {_specialCharge}";
        }
    }
    #endregion

    #region Subscribe to events
    void OnEnable()
    {
        _stateChangeChannelEvent.onEventRaised += ConvertEnumToState;
    }

    void OnDisable()
    {
        _stateChangeChannelEvent.onEventRaised -= ConvertEnumToState;
    }
    #endregion

    #region State transition
    private void ConvertEnumToState(PlayerState state)
    {
        IState nextState = null;
        switch (state)
        {
            case PlayerState.EggState:
                nextState = new EggState();
                break;
            case PlayerState.ChickState:
                nextState = new ChickState();
                break;
            case PlayerState.ChickenState:
                nextState = new ChickenState();
                break;
            case PlayerState.RoosterState:
                nextState = new RoosterState();
                break;
            case PlayerState.SuperRoosterState:
                nextState = new SuperRoosterState();
                break;
            case PlayerState.DinosaurState:
                nextState = new DinosaurState();
                break;
            default:
                break;
        }

        TransitionTo(nextState);
    }

    /// <summary>
    /// Initializes with our first state.
    /// </summary>
    /// <param name="startingState"></param>
    public void TransitionTo(IState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        nextState?.Enter(_playerPowerUp);
        _specialCharge = 1;
    }
    #endregion

    /// <summary>
    /// Each state has a different special capacity.
    /// </summary>
    public void Special()
    {
        if (CurrentState != null && _specialCharge > 0)
        {
            StartCoroutine(CurrentState.Special());
            _specialCharge--;
        }
    }
}
