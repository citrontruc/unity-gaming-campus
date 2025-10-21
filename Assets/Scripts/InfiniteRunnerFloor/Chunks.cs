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
    public Transform StartPoint;
    public Transform EndPoint;
    private Spawner _spawner => Singleton<Spawner>.Instance;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError($"Chunk '{gameObject.name}' is missing a Collider component! Please add one to the root GameObject.", this);
        }
    }

    public ChunkState GetChunkState()
    {
        return _chunkState;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        _chunkState = ChunkState.active;
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        _chunkState = ChunkState.disabled;
        _collider.enabled = false;
        _spawner.EnqueueChunk(this);
    }
}
