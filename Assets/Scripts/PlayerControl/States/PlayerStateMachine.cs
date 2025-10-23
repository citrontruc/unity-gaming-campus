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
        DinosaurState,
    }

    public IState CurrentState { get; private set; }
    private int _specialCharge = 1;

    public override void Awake()
    {
        base.Awake();
        CurrentState = new EggState();
    }

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
        nextState?.Enter();
        _specialCharge = 1;
        Debug.Log(_specialCharge);
    }

    public void Update() { }

    /// <summary>
    /// Each state has a different special capacity.
    /// </summary>
    public void Special()
    {
        Debug.Log(_specialCharge);
        if (CurrentState != null && _specialCharge > 0)
        {
            StartCoroutine(CurrentState.Special());
            _specialCharge--;
        }
    }
}
