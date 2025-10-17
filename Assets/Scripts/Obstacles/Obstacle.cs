using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [Header("Collision Events")]
    [SerializeField]
    private ObstacleCollisionEventChannelSO CollisionEvent;
    public enum Resistance
    {
        small = 0,
        large = 1,
        unbreakable = 2,
    }

    protected Resistance _resistance;

    [SerializeField]
    private string _playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            CollisionEvent?.RaiseEvent(_resistance);
        }
    }
}
