/*
A channel to handle collisions with obstacle objects.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "ObstacleCollisionEventChannel_SO",
    menuName = "Events/ObstacleCollisionEventChannelSO"
)]
public class ObstacleCollisionEventChannelSO : VoidEventChannelSO<Obstacle.Resistance> { }
