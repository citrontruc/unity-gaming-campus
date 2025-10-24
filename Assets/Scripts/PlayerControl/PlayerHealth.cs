/*
An object to keep a trace of the player health and activate effects related to health.
*/

using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    #region Event Channels
    [SerializeField]
    public NoHealthEventChannelSO _noHealthEvent;
    #endregion

    #region Player values
    private int _playerHealth = 1;
    #endregion

    #region Setters and Getters
    public void SetHealth(int healthValue)
    {
        _playerHealth = healthValue;
    }
    #endregion

    /// <summary>
    /// Certain PlayerStates can break fragile obstacles.
    /// </summary>
    /// <param name="resistance">Resistance of the obstacle we just hit.</param>
    public void CollisionWithObstacle(Obstacle.Resistance resistance)
    {
        _playerHealth -= 1;
        if (_playerHealth <= 0)
        {
            _noHealthEvent?.RaiseEvent(0);
        }
    }
}
