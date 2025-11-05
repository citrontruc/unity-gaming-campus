/*
The SuperRoosterState. it is the fifth state the player is in.
*/

using System.Collections;
using UnityEngine;

public class SuperRoosterState : IState
{
    private PlayerStateMachine _playerStateMachine;
    private int _powerUpDuration = 10;

    public void Enter(PlayerStateMachine playerStateMachine, PlayerPowerUp playerPowerUp)
    {
        playerPowerUp.EnablePower(PlayerPowerUp.PlayerPowerEnum.Dash);
        _playerStateMachine = playerStateMachine;
    }

    public void Update() { }

    /// <summary>
    /// Implement special effect here.
    /// Real effect not implemented here. We use the Chick's effect right now.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Special()
    {
        _playerStateMachine.SpecialAnimation(true);
        _playerStateMachine.InvokeChangeLevelSpeed(.5f);
        yield return new WaitForSeconds(_powerUpDuration);
        _playerStateMachine.InvokeChangeLevelSpeed(2f);
        _playerStateMachine.SpecialAnimation(false);
    }

    public void Exit() { }
}
