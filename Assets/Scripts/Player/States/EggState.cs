/*
The Eggstate. it is the first state the player is in.
It's special makes him transform in a super egg that can withstand one blow.
*/

using System.Collections;
using UnityEngine;

public class EggState : IState
{
    private PlayerHealth _player => PlayerHealth.Instance;
    private int _powerUpDuration = 10;

    public void Enter(PlayerPowerUp playerPowerUp) { }

    public void Update() { }

    /// <summary>
    /// The eggstate's special effect is to let the player withstand a blow.
    /// We achieve that by making the player have two hitpoints for a limited amount of time.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Special(PlayerAnimator animator)
    {
        animator.SetSpecial(true);
        _player.SetHealth(2);
        yield return new WaitForSeconds(_powerUpDuration);
        _player.SetHealth(1);
        animator.SetSpecial(false);
    }

    public void Exit() { }
}
