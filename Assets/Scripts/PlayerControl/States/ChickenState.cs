using System.Collections;
using UnityEngine;

public class ChickenState : IState
{
    private PlayerStateMachine _playerStateMachine;
    private int _powerUpDuration = 10;

    public void Enter()
    {
        PlayerPowerUp.Instance.EnablePower(PlayerPowerUp.PlayerPowerEnum.Glide);
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
