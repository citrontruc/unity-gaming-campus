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

    [SerializeField]
    private ChunkCharacteristicsSO _chunkRarity;

    private ChunkState _chunkState = ChunkState.disabled;
    private Collider _collider;
    public Transform StartPoint;
    public Transform EndPoint;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError(
                $"Chunk '{gameObject.name}' is missing a Collider component! Please add one to the root GameObject.",
                this
            );
        }
    }

    public ChunkState GetChunkState()
    {
        return _chunkState;
    }

    public int GetChunkRarity()
    {
        return _chunkRarity.NumApparitions;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        _chunkState = ChunkState.active;

        ActivateChildComponent<Collectible>();
    }

    private void ActivateChildComponent<T>() where T : Component
    {
        T[] componentList = GetComponentsInChildren<T>(true);
        foreach (T component in componentList)
        {
            if (component is IActivatable activatable)
            {
                activatable.Activate();
            }
        }
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        _chunkState = ChunkState.disabled;

        DeactivateChildComponent<Collectible>();
    }

    private void DeactivateChildComponent<T>() where T : Component
    {
        T[] componentList = GetComponentsInChildren<T>(true);
        foreach (T component in componentList)
        {
            if (component is IActivatable activatable)
            {
                activatable.Deactivate();
            }
        }
    }
}
