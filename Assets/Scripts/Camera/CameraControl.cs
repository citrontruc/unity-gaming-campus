using UnityEngine;

public class Follow_player : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Vector3 _lagDistance = new(0, 10, -7);

    /// <summary>
    /// Camera is fixed on the y axis to avoid violent movements on the y axis.
    /// We follow the player object.
    /// </summary>
    void LateUpdate()
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 positionReworked = new(playerPosition.x, 0f, playerPosition.z);
        transform.position = positionReworked + _lagDistance;
    }
}
