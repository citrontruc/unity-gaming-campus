/*
A Scriptable object to give change the speed of the level.
*/

using UnityEngine;

[CreateAssetMenu(fileName = "ChangeLevelSpeed_SO", menuName = "Events/ChangeLevelSpeedSO")]
public class ChangeLevelSpeedSO : VoidEventChannelSO<float>
{
    public float MultiplicationCoefficient;
}
