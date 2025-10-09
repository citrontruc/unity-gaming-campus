/* An abstract class in order to define states for our player.
*/

/// <summary>
/// Unity has its own StateMachineBehaviour states.
/// In our case, we did not need the extra complexity.
/// </summary>
public interface IState
{
    void Enter(PlayerController player);
    void Update(PlayerController player);
    void Exit(PlayerController player);
}
