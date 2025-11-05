/*
A state machine to handle player state transition.
Also handles special powers (each state is connected to the player state machine and have access to the event channels).
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
    
    #region Event Channels
    [SerializeField]
    private StateChangeEventChannelSO _stateChangeChannelEvent;

    [SerializeField]
    private SpecialReloadEventChannelSO _specialReloadChannelEvent;

    [SerializeField]
    private ChangeLevelSpeedSO _changeLevelSpeedChannelEvent;

    [SerializeField]
    private SpecialAnimationChannelSO _specialAnimationChannelEvent;

    [SerializeField]
    private SetHealthEventChannelSO _setHealthChannelEvent;
    #endregion

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
        IState eggState = new EggState();
        TransitionTo(eggState);
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
        _specialReloadChannelEvent.onEventRaised += SetSpecialCharge;
    }

    void OnDisable()
    {
        _stateChangeChannelEvent.onEventRaised -= ConvertEnumToState;
        _specialReloadChannelEvent.onEventRaised -= SetSpecialCharge;
    }

    public void InvokeChangeLevelSpeed(float value)
    {
        _changeLevelSpeedChannelEvent.RaiseEvent(value);
    }

    public void SpecialAnimation(bool value)
    {
        _specialAnimationChannelEvent.RaiseEvent(value);
    }

    public void SetHealth(int value)
    {
        _setHealthChannelEvent.RaiseEvent(value);
    }

    public void SetSpecialCharge(int value)
    {
        _specialCharge = value;
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
        nextState?.Enter(this, _playerPowerUp);
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
