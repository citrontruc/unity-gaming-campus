/*
Event to notify if the user's score should increase.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "SpecialReloadEventChannel_SO",
    menuName = "Events/SpecialReloadEventChannelSO"
)]
public class SpecialReloadEventChannelSO : VoidEventChannelSO<int> { }
