/*
A chunk object that can be moved around.
It can be enabled or disabled.
*/

using UnityEngine;

public class Chunk : MonoBehaviour
{
    public enum ChunkState
    {
        active,
        disabled,
    }

    private ChunkState _chunkState = ChunkState.disabled;
    private Collider _collider;

    void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void Activate()
    {
        _chunkState = ChunkState.active;
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        _chunkState = ChunkState.disabled;
        _collider.enabled = false;
    }
}
