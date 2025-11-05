/*
A class to destroy chunks that leave the screen.
*/

using UnityEngine;

public class ChunkDestroyer : MonoBehaviour
{
    [SerializeField]
    private string _tagToDestroy = "Chunk";

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    /// <summary>
    /// The destroyer deactivates any chunk it touches.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _tagToDestroy)
        {
            other.GetComponent<Chunk>().Deactivate();
        }
    }
}
