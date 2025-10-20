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
    private int _randomSeed = 42;

    /// <summary>
    /// We set our seed for randomness in the Awake method.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        Random.InitState(_randomSeed);
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
