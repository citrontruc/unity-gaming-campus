/*
Event to notify that the user has pressed the move button.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "MoveEventChannel_SO",
    menuName = "Events/MoveEventChannelSO"
)]
public class MoveEventChannelSO : VoidEventChannelSO<Vector2> { }
