/*
A class to spawn new chunks and choose which chunks to use next.
*/

using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    /// <summary>
    /// We have a list of chunks and every time, we want to generate tile,
    /// we take a tile at random from our queue.
    /// </summary>
    private List<Chunk> _spawnQueue;
    private List<Chunk> _activeChunkList;
    private int _randomSeed = 42;

    [SerializeField]
    private float _levelSpeed = 5f;
    public Transform SpawnPoint;

    public enum ChunkType
    {
        BasicChunk
    }

    [SerializeField]
    private Dictionary<ChunkType, int> _chunkRepartition = new();

    /// <summary>
    /// We set our seed for randomness in the Awake method.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        Random.InitState(_randomSeed);
        SpawnPoint = this.transform;
        Initilialize();
    }

    private void Initilialize()
    {
        Object[] chunks = Resources.LoadAll("Prefabs/Chunks", typeof(Chunk));
        _chunkRepartition[ChunkType.BasicChunk] = 20;
        foreach (KeyValuePair<ChunkType, int> chunk in _chunkRepartition)
        {
            
        }
    }

    private Chunk GetRandomChunk()
    {
        int chunkPosition = Random.Range(0, _spawnQueue.Count);
        Chunk selectedChunk = _spawnQueue[chunkPosition];
        _spawnQueue.RemoveAt(chunkPosition);
        return selectedChunk;
    }

    private void EnqueueChunk(Chunk chunk)
    {
        _spawnQueue.Add(chunk);
    }
}
