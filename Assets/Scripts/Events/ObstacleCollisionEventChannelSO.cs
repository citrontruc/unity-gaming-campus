/*
A channel to handle collisions with obstacle objects.
*/

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "ObstacleCollisionEventChannel_SO",
    menuName = "Events/ObstacleCollisionEventChannelSO"
)]
public class ObstacleCollisionEventChannelSO : ScriptableObject
{
    /// <summary>
    /// Indicates if the collision was between a destructible object or not.
    /// </summary>
    public UnityAction onEventRaised;

    public void RaiseEvent()
    {
        onEventRaised?.Invoke();
    }
}
