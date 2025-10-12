using System.Collections;
using UnityEngine;

public class ChickenState : IState
{
    private PlayerDataBase _playerDataBase;
    private PlayerStateMachine _playerStateMachine;
    private int _powerUpDuration = 10;

    public void Enter() { }

    public void Update() { }

    /// <summary>
    /// The ChickenState's special effect is.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Special()
    {
        yield return null;
    }

    public void Exit() { }
}
