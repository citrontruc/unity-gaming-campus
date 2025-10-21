/*
A class to spawn new chunks and choose which chunks to use next.
*/

using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;

public class Spawner : Singleton<Spawner>
{
    /// <summary>
    /// We have a list of chunks and every time, we want to generate tile,
    /// we take a tile at random from our queue.
    /// </summary>
    private List<Chunk> _spawnQueue = new();
    private List<Chunk> _activeChunkList = new();
    private int _randomSeed = 42;

    [SerializeField]
    private float _levelSpeed = 5f;

    private int _numChunks;
    private int _chunkSize = 20;
    public Transform SpawnPoint => this.transform;
    public Transform Destroyer => Singleton<ChunkDestroyer>.Instance.transform;

    public enum ChunkType
    {
        BasicChunk,
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
        _numChunks = (int)(SpawnPoint.position.z - Destroyer.position.z) / _chunkSize + 1;
        Initilialize();
    }

    private void Initilialize()
    {
        Object[] chunks = Resources.LoadAll("Chunks", typeof(GameObject));
        Object currentVal;
        Chunk myChunk;
        for (int i = 0; i< chunks.Count(); i++)
        {
            for (int j=0; j< 5; j++)
            {
                currentVal = Instantiate(chunks[i], new Vector3(0, 0, 0), Quaternion.identity);
                myChunk = currentVal.GetComponent<Chunk>();
                myChunk.Deactivate();
            }
        }
    }

    public void Update()
    {
        Debug.Log($"ActiveChunks {_activeChunkList.Count}");
        Debug.Log(_numChunks);
        Debug.Log($"SpawnQueue {_spawnQueue.Count}");
        if (_activeChunkList.Count < _numChunks)
        {
            for (int i =0; i < _numChunks - _activeChunkList.Count; i++)
            {
                Chunk myChunk = GetRandomChunk();
                if (_activeChunkList.Count == 0)
                {
                    myChunk.transform.position = new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, Destroyer.position.z + _chunkSize);
                }
                else
                {
                    myChunk.transform.position = new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, _activeChunkList.Last().transform.position.z + _chunkSize);
                }
                myChunk.Activate();
                _activeChunkList.Add(myChunk);
            }
        }
        for (int i = _activeChunkList.Count - 1; i >= 0; i--)
        {
            Chunk myChunk = _activeChunkList[i];
            myChunk.transform.Translate(0, 0, -_levelSpeed * Time.deltaTime);
            if (myChunk.GetChunkState() == Chunk.ChunkState.disabled)
            {
                _activeChunkList.Remove(myChunk);
            }
        }
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
}
