/*
Event to notify that the user has pressed the jump button.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "JumpEventChannel_SO",
    menuName = "Events/JumpEventChannelSO"
)]
public class JumpEventChannelSO : VoidEventChannelSO<bool> { }
