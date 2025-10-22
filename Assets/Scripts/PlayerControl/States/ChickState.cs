/*
The ChickState. it is the second state the player is in.
It's special makes him slow down time for ten seconds.
*/

using System.Collections;
using UnityEngine;

public class ChickState : IState
{
    private Spawner _spawner => Spawner.Instance;
    private int _powerUpDuration = 10;

    public void Enter() { }

    public void Update() { }

    /// <summary>
    /// The eggstate's special effect is to let the player withstand a blow.
    /// We achieve that by making the player have two hitpoints for a limited amount of time.
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
