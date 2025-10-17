/*An object to store important information about the player.
It is a singleton meant to represent the player score, preferences and other.*/

using UnityEngine;

public class PlayerPowerUp : Singleton<PlayerPowerUp>
{
    /// <summary>
    /// Since the player Abilities are cumulative, we switch them on when the user changes states.
    /// </summary>
    #region Abilities
    [Header("Abilities")]
    private bool _enableSlide = false;

    [SerializeField]
    private bool _enableDoubleJump = false;
    private bool _enableGlide = false;
    private bool _enableDash = false;
    private bool _enableDestroySmallObstacles = false;
    #endregion

    #region Temporary abilities
    private bool _doubleJump = true;
    #endregion

    private int _playerHealth = 1;

    public void SetPlayerHealth(int healthValue)
    {
        _playerHealth = healthValue;
    }    

    public bool CanDoubleJump()
    {
        return _doubleJump && _enableDoubleJump;
    }

    public void ResetDoubleJump()
    {
        _doubleJump = true;
    }

    public void ResolveDoubleJump()
    {
        _doubleJump = false;
    }
}
