using System;
using UnityEngine;

[Serializable]
public class PlayerStateMachine : Singleton<PlayerStateMachine>
{

    [SerializeField]
    private StateChangeEventChannelSO _stateChangeChannelEvent;

    public enum PlayerState
    {
        EggState,
        ChickState,
        ChickenState,
        RoosterState,
        SuperRoosterState,
        DinosaurState
    }

    public IState CurrentState { get; private set; }

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
                nextState = new ChickState();
                break;
            case PlayerState.RoosterState:
                nextState = new ChickState();
                break;
            case PlayerState.SuperRoosterState:
                nextState = new ChickState();
                break;
            case PlayerState.DinosaurState:
            nextState = new ChickState();
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
        nextState?.Enter();
    }

    public void Update() { }

    /// <summary>
    /// Each state has a different special capacity.
    /// </summary>
    public void Special()
    {
        if (CurrentState != null)
        {
            StartCoroutine(CurrentState.Special());
        }
    }
}
