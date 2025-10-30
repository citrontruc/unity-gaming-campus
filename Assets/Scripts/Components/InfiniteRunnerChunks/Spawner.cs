/*
A class to spawn new chunks and move existing chunks.
*/

using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    #region Chunk Creation properties
    /// <summary>
    /// We have a list of chunks and every time, we want to generate tile,
    /// we take a tile at random from our queue.
    /// </summary>
    private List<Chunk> _spawnQueue = new();
    private List<Chunk> _activeChunkList = new();
    private int _randomSeed = 42;

    private int _numChunks;

    /// <summary>
    /// How many empty chunks do we have before we start having chunks with collectibles and Obstacles?
    /// </summary>
    private int _numChunksAtBeginning = 2;
    private string _chunkFolderName = "Chunks";
    private string _beginningChunkName = "EmptyChunk";
    private int _chunkSize = 20;
    public Transform SpawnPoint => this.transform;
    public Transform Destroyer => Singleton<ChunkDestroyer>.Instance.transform;
    #endregion

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

    public float GetLevelSpeed()
    {
        return _levelSpeed;
    }
    #endregion

    #region Subscribe to events
    void OnEnable()
    {
        _stateChangeChannelEvent.onEventRaised += SpeedUpLevel;
    }

    void OnDisable()
    {
        _stateChangeChannelEvent.onEventRaised -= SpeedUpLevel;
    }
    #endregion

    #region Monobehaviour methods
    /// <summary>
    /// We set our seed for randomness in the Awake method.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        Random.InitState(_randomSeed);
        _numChunks = (int)(SpawnPoint.position.z - Destroyer.position.z) / _chunkSize + 1;
        Initilialize();
    }

    private void Initilialize()
    {
        Object[] chunks = Resources.LoadAll(_chunkFolderName, typeof(GameObject));
        Object currentVal;
        Chunk myChunk;
        for (int i = 0; i < chunks.Count(); i++)
        {
            int NumApparitions = chunks[i].GetComponent<Chunk>().GetChunkRarity();
            for (int j = 0; j < NumApparitions; j++)
            {
                currentVal = Instantiate(chunks[i], new Vector3(0, 0, 0), Quaternion.identity);
                myChunk = currentVal.GetComponent<Chunk>();
                myChunk.Deactivate();
                EnqueueChunk(myChunk);
            }
        }

        Object beginningChunk = Resources.Load($"{_chunkFolderName}/{_beginningChunkName}");
        for (int i = 0; i < _numChunksAtBeginning; i++)
        {
            currentVal = Instantiate(beginningChunk, new Vector3(0, 0, 0), Quaternion.identity);
            myChunk = currentVal.GetComponent<Chunk>();
            AddChunkToActiveChunks(myChunk);
        }
    }

    public void Update()
    {
        if (_activeChunkList.Count < _numChunks)
        {
            for (int i = 0; i < _numChunks - _activeChunkList.Count; i++)
            {
                Chunk myChunk = GetRandomChunk();
                AddChunkToActiveChunks(myChunk);
            }
        }
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
                    EnqueueChunk(myChunk);
                }
            }
        }
    }
    #endregion

    #region Retrieve chunks from lists
    private void AddChunkToActiveChunks(Chunk chunk)
    {
        if (_activeChunkList.Count == 0)
        {
            chunk.transform.position = new Vector3(
                SpawnPoint.position.x,
                SpawnPoint.position.y,
                Destroyer.position.z + _chunkSize
            );
        }
        else
        {
            chunk.transform.position = new Vector3(
                SpawnPoint.position.x,
                SpawnPoint.position.y,
                _activeChunkList.Last().transform.position.z + _chunkSize
            );
        }
        chunk.Activate();
        _activeChunkList.Add(chunk);
    }

    private Chunk GetRandomChunk()
    {
        int chunkPosition = Random.Range(0, _spawnQueue.Count);
        Chunk selectedChunk = _spawnQueue[chunkPosition];
        _spawnQueue.Remove(selectedChunk);
        return selectedChunk;
    }

    public void EnqueueChunk(Chunk chunk)
    {
        _spawnQueue.Add(chunk);
    }
    #endregion
}
