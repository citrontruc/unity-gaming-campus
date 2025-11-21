/*
Event to notify the Animator that he should change the player Animation.
*/

using UnityEngine;

[CreateAssetMenu(
    fileName = "SpecialAnimationChannel_SO",
    menuName = "Events/SpecialAnimationChannelSO"
)]
public class SpecialAnimationChannelSO : VoidEventChannelSO<bool> { }
