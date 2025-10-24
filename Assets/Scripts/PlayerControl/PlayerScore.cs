/*
An object to keep a trace of the player score.
*/

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : Singleton<PlayerScore>
{
    #region Event Channels
    [SerializeField]
    private StateChangeEventChannelSO _stateChangeChannelEvent;
    #endregion

    #region Player score
    private int _playerScore = 0;
    #endregion

    #region Score related effects
    /// <summary>
    /// Write player score on screen.
    /// </summary>
    public TMP_Text ScoreText;

    [SerializeField]
    private Dictionary<int, PlayerStateMachine.PlayerState> _thresholdList = new Dictionary<
        int,
        PlayerStateMachine.PlayerState
    >()
    {
        { 50, PlayerStateMachine.PlayerState.ChickState },
        { 100, PlayerStateMachine.PlayerState.ChickenState },
        { 250, PlayerStateMachine.PlayerState.RoosterState },
        { 500, PlayerStateMachine.PlayerState.SuperRoosterState },
        { 1000, PlayerStateMachine.PlayerState.DinosaurState },
    };
    #endregion

    #region Setters and Getters
    public int GetScore()
    {
        return _playerScore;
    }
    #endregion

    #region Update score and activate corresponding effects
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
                _stateChangeChannelEvent?.RaiseEvent(entry.Value);
            }
        }
        ;
    }
    #endregion

    #region Monobehaviours methods
    void Update()
    {
        ScoreText.text = $"Score: {_playerScore}";
    }
    #endregion
}
