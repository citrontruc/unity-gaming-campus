/*
An object to handle active chunks and make sure
that our chunklist contains the right number of chunks.
*/

using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : Singleton<ChunkManager>
{
    private int _numChunks = 10;
    private List<Chunk> _activeChunkList;

    [SerializeField]
    private float _levelSpeed = 5f;
}
