/*An object to store important information about the player.
It is a singleton meant to represent the player score, preferences and other.*/

using UnityEngine;

public class PlayerDataBase
{
    /// <summary>
    /// Since the player Abilities are cumulative, we switch them on when the user changes states.
    /// </summary>
    [Header("Abilities")]
    private bool _enableSlide = false;
    private bool _enableDoubleJump = false;
    private bool _enableGlide = false;
    private bool _enableDash = false;
    private bool _enableDestroySmallObstacles = false;

    private static int _playerScore = 0;

    public static int GetScore()
    {
        return _playerScore;
    }

    public static void IncrementScore(int value)
    {
        _playerScore += value;
    }
}
