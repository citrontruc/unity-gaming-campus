/* Class to create collectible objects.*/

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class Collectible : MonoBehaviour
{
    [Header("Collision Events")]
    [SerializeField]
    private UnityEvent _onCollision; // We cacn add audio source and a lot of things.

    [SerializeField]
    private string _canCollectTag = "player";

    private protected int _value;

    public int GetValue()
    {
        return _value;
    }

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_canCollectTag))
        {
            _onCollision?.Invoke();
        }
    }
}
