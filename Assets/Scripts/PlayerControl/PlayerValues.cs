/*
An object to keep a trace of the player score and update it.
*/

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerValues : Singleton<PlayerValues>
{
    public NoHealthEventChannelSO _noHealthEvent;
    public TMP_Text ScoreText;
    private int _playerScore = 0;
    private int _playerHealth = 1;

    [SerializeField]
    private Dictionary<int, PlayerStateMachine.PlayerState> _thresholdList = new Dictionary<int, PlayerStateMachine.PlayerState>()
    {
        { 50, PlayerStateMachine.PlayerState.ChickState },
        { 100, PlayerStateMachine.PlayerState.ChickenState },
        { 250, PlayerStateMachine.PlayerState.RoosterState },
        { 500, PlayerStateMachine.PlayerState.SuperRoosterState },
        { 1000, PlayerStateMachine.PlayerState.DinosaurState },
    };

    #region Setters and Getters
    public int GetScore()
    {
        return _playerScore;
    }

    public void SetHealth(int healthValue)
    {
        _playerHealth = healthValue;
    }
    #endregion

    public void IncrementScore(int value)
    {
        _playerScore += value;
        CheckThreshold(value);
    }

    public void CheckThreshold(int value)
    {
        foreach (KeyValuePair<int, PlayerStateMachine.PlayerState> entry in _thresholdList)
        {
            if (entry.Key <= _playerScore && _playerScore - value < entry.Key)
            {
                
            }
        }
        ;
    }

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

    #region Monobehaviours methods
    void Update()
    {
        ScoreText.text = $"Score: {_playerScore}";
    }
    #endregion
}
