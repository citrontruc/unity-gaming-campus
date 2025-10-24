/*
The RoosterState. it is the fourth state the player is in.
*/

using System.Collections;
using UnityEngine;

public class RoosterState : IState
{
    private Spawner _spawner => Spawner.Instance;
    private int _powerUpDuration = 10;

    public void Enter()
    {
        PlayerPowerUp.Instance.EnablePower(PlayerPowerUp.PlayerPowerEnum.DoubleJump);
    }

    public void Update() { }

    /// <summary>
    /// Implement special effect here.
    /// Real effect not implemented here. We use the Chick's effect right now.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Special()
    {
        _spawner.MultiplyLevelSpeed(.5f);
        yield return new WaitForSeconds(_powerUpDuration);
        _spawner.MultiplyLevelSpeed(2f);
    }

    public void Exit() { }
}
