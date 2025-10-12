/*
The Eggstate. it is the first state the player is in.
It's special makes him transform in a super egg that can withstand one blow.
*/

using System.Collections;

public class EggState : IState
{
    private PlayerController _player;
    public void Enter()
    {
        
    }

    public void Update()
    {

    }

    public IEnumerator Special()
    {
        yield return null;
    }
    
    public void Exit()
    {
        
    }
}