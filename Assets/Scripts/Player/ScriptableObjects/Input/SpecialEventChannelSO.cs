/*
Event to notify that the user has pressed the special button.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "SpecialEventChannel_SO",
    menuName = "Events/SpecialEventChannelSO"
)]
public class SpecialEventChannelSO : VoidEventChannelSO<bool> { }
