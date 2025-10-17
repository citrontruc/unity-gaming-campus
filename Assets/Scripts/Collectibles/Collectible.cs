/* Class to create collectible objects.*/

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class Collectible : MonoBehaviour
{
    [Header("Collision Events")]
    [SerializeField]
    private ScoreEventChannelSO CollectedEvent;

    [SerializeField]
    private string _canCollectTag = "Player";

    [SerializeField]
    protected int _value;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_canCollectTag))
        {
            CollectedEvent?.RaiseEvent(_value);
        }
    }
}
