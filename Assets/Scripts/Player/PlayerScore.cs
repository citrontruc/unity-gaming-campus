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

    [SerializeField]
    private HighScoreEventChannelSO _highScoreEventChannelEvent;

    [SerializeField]
    private NoHealthEventChannelSO _noHealthEvent;
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

    public void BroadCastScore(int health)
    {
        _highScoreEventChannelEvent.RaiseEvent(_playerScore);
    }
    #endregion

    #region Monobehaviours methods
    void OnEnable()
    {
        _noHealthEvent.onEventRaised += BroadCastScore;
    }

    void OnDisable()
    {
        _noHealthEvent.onEventRaised -= BroadCastScore;
    }

    void Update()
    {
        if (ScoreText != null)
        {
            ScoreText.text = $"Score: {_playerScore}";
        }
    }
    #endregion
}
