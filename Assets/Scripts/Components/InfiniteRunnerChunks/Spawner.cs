/*
A class to spawn new chunks and move existing chunks.
*/

using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField]
    private ChunkMover _chunkMover;

    #region Chunk Creation properties
    /// <summary>
    /// We have a list of chunks and every time, we want to generate tile,
    /// we take a tile at random from our queue.
    /// </summary>
    private List<Chunk> _spawnQueue = new();
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
    public Transform Destroyer;
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
        Destroyer = Singleton<ChunkDestroyer>.Instance.transform;
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
        if (_chunkMover.GetNumberActiveChunks() < _numChunks)
        {
            for (int i = 0; i < _numChunks - _chunkMover.GetNumberActiveChunks(); i++)
            {
                Chunk myChunk = GetRandomChunk();
                AddChunkToActiveChunks(myChunk);
            }
        }
    }
    #endregion

    #region Retrieve chunks from lists
    private void AddChunkToActiveChunks(Chunk chunk)
    {
        if (_chunkMover.GetNumberActiveChunks() == 0)
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
                _chunkMover.GetLastChunkPosition().z + _chunkSize
            );
        }
        chunk.Activate();
        _chunkMover.AddChunk(chunk);
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
