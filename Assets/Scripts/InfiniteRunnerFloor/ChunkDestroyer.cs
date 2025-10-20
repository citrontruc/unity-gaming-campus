using UnityEngine;

public class ChunkDestroyer : Singleton<ChunkDestroyer>
{
    [SerializeField]
    private string _tagToDestroy = "Chunk";

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Put it back in a queue instead of destroying it.
        if (other.tag == _tagToDestroy)
        {
            other.GetComponent<Chunk>().Deactivate();
            Debug.Log("Contact with chunk");
        }
        Debug.Log("Contact");
        //Destroy(other.gameObject);
    }
}
