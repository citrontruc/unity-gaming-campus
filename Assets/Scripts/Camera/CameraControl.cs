using UnityEngine;

public class Follow_player : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Vector3 _lagDistance = new(0, 4, -7);

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 positionReworked = new(playerPosition.x, 0f, playerPosition.z);
        transform.position = positionReworked + _lagDistance;
    }
}
