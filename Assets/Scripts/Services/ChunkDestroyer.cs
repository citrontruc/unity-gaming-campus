using UnityEngine;

public class ChunkDestroyer : Singleton<ChunkDestroyer>
{
    private void OnTriggerEnter(Collider other)
    {
        // Put it back in a queue instead of destroying it.
        Destroy(other.gameObject);
    }
}
