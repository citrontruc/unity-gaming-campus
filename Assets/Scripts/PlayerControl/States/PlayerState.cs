/* An abstract class in order to define states for our player.
*/

using System.Collections;

/// <summary>
/// Unity has its own StateMachineBehaviour states.
/// In our case, we did not need the extra complexity.
/// </summary>
public interface IState
{
    public void Enter();
    public void Update();
    public IEnumerator Special();
    public void Exit();
}
