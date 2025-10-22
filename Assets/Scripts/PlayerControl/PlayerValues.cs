/*
An object to keep a trace of the player score and update it.
*/

using UnityEngine;
using TMPro;

public class PlayerValues : Singleton<PlayerValues>
{
    public TMP_Text ScoreText;
    private int _playerScore = 0;
    private int _playerHealth = 1;

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
    }

    /// <summary>
    /// Certain PlayerStates can break fragile obstacles.
    /// </summary>
    /// <param name="resistance">Resistance of the obstacle we just hit.</param>
    public void CollisionWithObstacle(Obstacle.Resistance resistance)
    {
        _playerHealth -= 1;
        Debug.Log($"Health {_playerHealth}");
    }

    #region Monobehaviours methods
    void Update()
    {
        ScoreText.text = $"Score: {_playerScore}";
    }
    #endregion
}
