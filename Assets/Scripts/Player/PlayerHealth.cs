/*
An object to keep a trace of the player health and activate effects related to health.
*/

using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    #region Event Channels
    [SerializeField]
    private NoHealthEventChannelSO _noHealthEvent;
    [SerializeField]
    private SetHealthEventChannelSO _setHealthChannelEvent;
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

    #region Subscribe to events
    void OnEnable()
    {
        _setHealthChannelEvent.onEventRaised += SetHealth;
    }

    void OnDisable()
    {
        _setHealthChannelEvent.onEventRaised += SetHealth;
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
