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
        Dinosaur
    }
    
    public IState CurrentState { get; private set; }

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

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}