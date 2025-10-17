using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel_SO", menuName = "Events/VoidEventChannelSO")]
public class VoidEventChannelSO<T> : ScriptableObject
{
    public UnityAction<T> onEventRaised;

    public void RaiseEvent(T value)
    {
        onEventRaised?.Invoke(value);
    }
}
