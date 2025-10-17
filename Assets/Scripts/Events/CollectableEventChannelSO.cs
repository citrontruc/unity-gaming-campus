using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "CollectableEventChannel_SO",
    menuName = "Events/CollectableEventChannelSO"
)]
public class CollectableEventChannelSO : ScriptableObject
{
    public int point;
    public UnityAction<int> onEventRaised;

    public void RaiseEvent(int value)
    {
        onEventRaised?.Invoke(value);
    }
}
