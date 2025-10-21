/*
A class to spawn new chunks and choose which chunks to use next.
*/

using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    /// <summary>
    /// We have a list of chunks and every time, we want to generate tile,
    /// we take a tile at random from our queue.
    /// </summary>
    private List<Object> _spawnQueue = new();
    private List<Object> _activeChunkList = new();
    private int _randomSeed = 42;

    [SerializeField]
    private float _levelSpeed = 5f;
    public Transform SpawnPoint;

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
        SpawnPoint = this.transform;
        Initilialize();
    }

    private void Initilialize()
    {
        Object[] chunks = Resources.LoadAll("Chunks", typeof(GameObject));
        Object currentVal = Instantiate(chunks[0], new Vector3(0, 0, 20), Quaternion.identity);
        Chunk myChunk = currentVal.GetComponent<Chunk>();
        myChunk.Activate();
        _activeChunkList.Add(currentVal);
        currentVal = Instantiate(chunks[1], new Vector3(0, 0, 40), Quaternion.identity);
        myChunk = currentVal.GetComponent<Chunk>();
        myChunk.Activate();
        _activeChunkList.Add(currentVal);
        /*
        foreach (KeyValuePair<ChunkType, int> chunk in _chunkRepartition)
        {
            Instantiate(chunks[0], new Vector3(0, 0, 20), Quaternion.identity);
        }
        */
    }

    public void Update()
    {
        if (_spawnQueue.Count > 0)
        {
            Object currentVal = Instantiate(_spawnQueue[0], new Vector3(0, 0, 40), Quaternion.identity);
            _spawnQueue.RemoveAt(0);
            Chunk myChunk = currentVal.GetComponent<Chunk>();
            myChunk.Activate();
            _activeChunkList.Add(currentVal);
        }
        for (int i = 0; i < _activeChunkList.Count(); i++)
        {
            Object currentVal = _activeChunkList[i];
            Chunk myChunk = currentVal.GetComponent<Chunk>();
            myChunk.transform.Translate(0, 0, -10f * Time.deltaTime);
            if (myChunk.GetChunkState() == Chunk.ChunkState.disabled)
            {
                _activeChunkList.RemoveAt(i);
            }
        }
    }
/*
    private Chunk GetRandomChunk()
    {
        int chunkPosition = Random.Range(0, _spawnQueue.Count);
        Object selectedChunk = _spawnQueue[chunkPosition];
        _spawnQueue.RemoveAt(chunkPosition);
        return selectedChunk;
    }
*/
    public void EnqueueChunk(Chunk chunk)
    {
        _spawnQueue.Add(chunk);
    }
}
