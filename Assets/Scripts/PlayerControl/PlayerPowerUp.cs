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

    [SerializeField]
    private bool _enableGlide = false;

    [SerializeField]
    private bool _enableDash = false;
    private bool _enableDestroySmallObstacles = false;
    #endregion

    public bool CanDoubleJump()
    {
        return _enableDoubleJump;
    }

    public bool CanDash()
    {
        return _enableDash;
    }

    public bool CanGlide()
    {
        return _enableGlide;
    }
}
