/*
Event to notify if the user's score should increase.
*/

using UnityEngine;

[CreateAssetMenu(fileName = "ScoreEventChannel_SO", menuName = "Events/ScoreEventChannelSO")]
public class ScoreEventChannelSO : VoidEventChannelSO<int> { }
