/*An object to store important information about the player.
It is a singleton meant to represent the player score, preferences and other.*/

using UnityEngine;

public class PlayerDataBase : Singleton<PlayerDataBase>
{
    /// <summary>
    /// Since the player Abilities are cumulative, we switch them on when the user changes states.
    /// </summary>
    #region Abilities
    [Header("Abilities")]
    private bool _enableSlide = false;
    private bool _enableDoubleJump = false;
    private bool _enableGlide = false;
    private bool _enableDash = false;
    private bool _enableDestroySmallObstacles = false;
    #endregion

    private int _playerHealth = 1;

    private int _playerScore = 0;

    public void SetPlayerHealth(int healthValue)
    {
        _playerHealth = healthValue;
    }

    public int GetScore()
    {
        return _playerScore;
    }

    public void IncrementScore(int value)
    {
        _playerScore += value;
    }
}
