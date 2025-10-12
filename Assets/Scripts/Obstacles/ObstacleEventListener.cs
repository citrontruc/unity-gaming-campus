/*
A script to listen to events related to obstacles and transmit the consequences to our Obstacle Handler.
*/

using UnityEngine;

public class ObstacleEventListener : Singleton<ObstacleEventListener>
{
    [Header("Collision Events")]
    [SerializeField]
    private ObstacleCollisionEventChannelSO CollisionEvent;

    [SerializeField]
    private string _playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            CollisionEvent?.RaiseEvent();
        }
    }
}