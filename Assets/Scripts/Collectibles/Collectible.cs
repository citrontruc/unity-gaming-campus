/* Class to create collectible objects.*/

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class Collectible : MonoBehaviour
{
    [Header("Collision Events")]
    [SerializeField]
    private CollectableEventChannelSO CollectedEvent;

    [SerializeField]
    private string _canCollectTag = "Player";

    protected int _value => GetValue();

    public int GetValue()
    {
        return CollectedEvent.point;
    }

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_canCollectTag))
        {
            CollectedEvent?.RaiseEvent(_value);
        }
    }
}
