using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject playerModel;

    [SerializeField]
    private Animator _playerAnimator;
    private Spawner _spawner => Singleton<Spawner>.Instance;
    private int _rotationHead = 10;
    private float _rotationThreshold = 0.1f;
    private float _speedThreshold = 6f;

    // Player should instantly be in the air but in order to avid jarring effects,
    // we set a maxJumpSpeed.
    private float _maxJumpSpeed = 0.6f;

    public void Update()
    {
        _playerAnimator.SetFloat("Speed", _spawner.GetLevelSpeed() / _speedThreshold);
    }

    public void MoveModel(Vector3 position)
    {
        position.y = math.min(position.y, playerModel.transform.position.y + _maxJumpSpeed);
        playerModel.transform.position = position;
    }

    public void RotateHead(float positionX)
    {
        if (math.abs(positionX - playerModel.transform.position.x) > _rotationThreshold)
        {
            playerModel.transform.localRotation = quaternion.Euler(0, _rotationHead, 0);
        }
    }

    public void SetSpecial(bool specialValue)
    {
        _playerAnimator.SetBool("Special", specialValue);
    }

    public void SetJump(bool jumpValue)
    {
        _playerAnimator.SetBool("Jump", jumpValue);
    }
}
