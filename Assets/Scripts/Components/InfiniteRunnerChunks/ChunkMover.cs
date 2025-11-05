/*
An object whose role is to move level chunks toward the player.
He receives Chunks from the ChunkSpawner and sends a signal when a chunk is destroyed
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkMover : Singleton<ChunkMover>
{
    [SerializeField]
    private Spawner _spawner;
    private List<Chunk> _activeChunkList = new();

    #region Movement related properties
    [SerializeField]
    private float _levelSpeed = 5f;

    [SerializeField]
    private float _speedUpFactor = 1.2f;

    /// <summary>
    /// We use the state change to know when to speed up the game.
    /// </summary>
    [SerializeField]
    private StateChangeEventChannelSO _stateChangeChannelEvent;

    [SerializeField]
    private ChangeLevelSpeedSO _changeLevelSpeedChannelEvent;
    #endregion

    #region Getters and Setters
    public void MultiplyLevelSpeed(float value)
    {
        _levelSpeed *= value;
    }

    public void SpeedUpLevel(PlayerStateMachine.PlayerState value)
    {
        MultiplyLevelSpeed(_speedUpFactor);
    }

    public void SpeedUpLevel(float value)
    {
        MultiplyLevelSpeed(value);
    }

    public float GetLevelSpeed()
    {
        return _levelSpeed;
    }

    public int GetNumberActiveChunks()
    {
        return _activeChunkList.Count;
    }

    public void AddChunk(Chunk chunk)
    {
        _activeChunkList.Add(chunk);
    }

    public Vector3 GetLastChunkPosition()
    {
        return _activeChunkList.Last().transform.position;
    }
    #endregion

    #region Subscribe to events
    /// <summary>
    /// The level speed changes either when the state of the player changes or when the player uses a special power.
    /// </summary>
    void OnEnable()
    {
        _stateChangeChannelEvent.onEventRaised += SpeedUpLevel;
        _changeLevelSpeedChannelEvent.onEventRaised += SpeedUpLevel;
    }

    void OnDisable()
    {
        _stateChangeChannelEvent.onEventRaised -= SpeedUpLevel;
        _changeLevelSpeedChannelEvent.onEventRaised -= SpeedUpLevel;
    }
    #endregion

    public void Update()
    {
        for (int i = _activeChunkList.Count - 1; i >= 0; i--)
        {
            Chunk myChunk = _activeChunkList[i];
            myChunk.transform.Translate(0, 0, -_levelSpeed * Time.deltaTime);
            if (myChunk.GetChunkState() == Chunk.ChunkState.disabled)
            {
                _activeChunkList.Remove(myChunk);
                int NumApparitions = myChunk.GetChunkRarity();
                if (NumApparitions > 0)
                {
                    _spawner.EnqueueChunk(myChunk);
                }
            }
        }
    }
}
