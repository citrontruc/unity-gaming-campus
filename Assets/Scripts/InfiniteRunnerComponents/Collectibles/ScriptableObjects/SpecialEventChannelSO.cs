/*
Event to notify if the user's special power should be replenished.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "SpecialReloadEventChannel_SO",
    menuName = "Events/SpecialReloadEventChannelSO"
)]
public class SpecialReloadEventChannelSO : VoidEventChannelSO<int> { }
