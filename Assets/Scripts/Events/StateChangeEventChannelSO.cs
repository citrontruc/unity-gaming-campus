/*
A channel to user state change.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "StateChangeEventChannel_SO",
    menuName = "Events/StateChangeEventChannelSO"
)]
public class StateChangeEventChannelSO : VoidEventChannelSO<PlayerStateMachine.PlayerState> { }
