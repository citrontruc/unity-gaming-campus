/*
An object to store important information about the player.
It is a singleton meant to represent the player score, preferences and other.
*/

using UnityEngine;

public class PlayerPowerUp : Singleton<PlayerPowerUp>
{
    [SerializeField]
    private StateChangeEventChannelSO _stateChangeChannelEvent;

    /// <summary>
    /// Since the player Abilities are cumulative, we switch them on when the user changes states.
    /// </summary>
    #region Abilities
    public enum PlayerPowerEnum
    {
        Slide,
        Glide,
        DoubleJump,
        Dash,
        DestroySmallObjects,
    }

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

    #region Getters and Setters
    public void EnablePower(PlayerPowerEnum playerPower)
    {
        switch (playerPower)
        {
            case PlayerPowerEnum.Slide:
                _enableSlide = true;
                break;
            case PlayerPowerEnum.Glide:
                _enableGlide = true;
                break;
            case PlayerPowerEnum.DoubleJump:
                _enableDoubleJump = true;
                break;
            case PlayerPowerEnum.Dash:
                _enableDash = true;
                break;
            case PlayerPowerEnum.DestroySmallObjects:
                _enableDestroySmallObstacles = true;
                break;
            default:
                break;
        }
    }

    public bool CanSlide()
    {
        return _enableSlide;
    }

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

    public bool CanDestroySmallObjects()
    {
        return _enableDestroySmallObstacles;
    }
    #endregion
}
