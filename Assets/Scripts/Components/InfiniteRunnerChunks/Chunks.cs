/*
A chunk object that can be moved around.
It can be enabled or disabled.
*/

using UnityEngine;

public class Chunk : MonoBehaviour
{
    #region State informations
    public enum ChunkState
    {
        active,
        disabled,
    }
    private ChunkState _chunkState = ChunkState.disabled;
    #endregion

    [SerializeField]
    private ChunkCharacteristicsSO _chunkRarity;

    #region Chunk components
    private Collider _collider;
    public Transform StartPoint;
    public Transform EndPoint;
    #endregion

    #region Getters and Setters
    public ChunkState GetChunkState()
    {
        return _chunkState;
    }

    public int GetChunkRarity()
    {
        return _chunkRarity.NumApparitions;
    }
    #endregion

    #region Monobehaviour methods
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
    #endregion

    #region Activate and deactivate chunks
    public void Activate()
    {
        this.gameObject.SetActive(true);
        _chunkState = ChunkState.active;

        ActivateChildComponent<Collectible>();
    }

    private void ActivateChildComponent<T>()
        where T : Component
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

    private void DeactivateChildComponent<T>()
        where T : Component
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
    #endregion
}
