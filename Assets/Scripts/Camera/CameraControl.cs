using UnityEngine;

public class Follow_player : MonoBehaviour {

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Vector3 _lagDistance = new(0, 3, -7);
    

    // Update is called once per frame
    void LateUpdate () {
        transform.position = _player.transform.position + _lagDistance;
    }
}