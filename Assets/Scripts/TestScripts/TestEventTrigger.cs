/*
A test script to raise an event and test the response of the components linked to the event.
*/

using UnityEngine;


public class TestEventRaise : MonoBehaviour
{
    [SerializeField] private CollectableEventChannelSO channel;

    void Start()
    {
        channel.RaiseEvent();
    }
}
