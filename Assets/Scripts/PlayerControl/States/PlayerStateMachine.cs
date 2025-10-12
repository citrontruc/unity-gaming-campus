using System;
using UnityEngine;

[Serializable]
public class StateMachine
{
    public enum PosibleState
    {
        Egg,
        Chick,
        Chicken,
        Rooster,
        SuperRooster,
        Dinosaur,
    }

    public IState CurrentState { get; private set; }

    /// <summary>
    /// Initializes with our first state.
    /// </summary>
    /// <param name="startingState"></param>
    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    /// <summary>
    /// Our States give additional powers to our main character.
    /// We don't have any additional update to do.
    /// </summary>
    public void Update()
    {
        
    }

    /// <summary>
    /// Each state has a different special capacity.
    /// </summary>
    public void Special()
    {
        if (CurrentState != null)
        {
            CurrentState.Special();
        }
    }
}
