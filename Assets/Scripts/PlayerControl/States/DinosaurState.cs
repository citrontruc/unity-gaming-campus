/*
The DinosaurState. it is the last state the player is in.
*/

using System.Collections;
using UnityEngine;

public class DinosaurState : IState
{
    private PlayerStateMachine _playerStateMachine;
    private int _powerUpDuration = 10;

    public void Enter()
    {
        PlayerPowerUp.Instance.EnablePower(PlayerPowerUp.PlayerPowerEnum.DestroySmallObjects);
    }

    public void Update() { }

    /// <summary>
    /// Implement special effect here.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Special()
    {
        yield return null;
    }

    public void Exit() { }
}
