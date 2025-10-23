/*
A channel to handle collisions with obstacle objects.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "StateChangeEventChannel_SO",
    menuName = "Events/StateChangeEventChannelSO"
)]
public class StateChangeEventChannelSO : VoidEventChannelSO<PlayerStateMachine.PlayerState> { }
