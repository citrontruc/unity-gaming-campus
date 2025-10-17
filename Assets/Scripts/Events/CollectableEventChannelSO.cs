using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "CollectableEventChannel_SO",
    menuName = "Events/CollectableEventChannelSO"
)]
public class CollectableEventChannelSO<T> : ScriptableObject
{
    public UnityAction<T> onEventRaised;

    public void RaiseEvent(T value)
    {
        onEventRaised?.Invoke(value);
    }
}
