using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject playerModel;

    [SerializeField]
    private SpecialAnimationChannelSO _specialAnimationChannelEvent;

    [SerializeField]
    private Animator _playerAnimator;
    private ChunkMover _chunkMover => Singleton<ChunkMover>.Instance;
    private float _speedThreshold = 6f;

    // Player should instantly be in the air but in order to avid jarring effects,
    // we set a maxJumpSpeed.
    private float _maxJumpSpeed = 0.6f;

    #region Subscribe to events
    void OnEnable()
    {
        _specialAnimationChannelEvent.onEventRaised += SetSpecial;
    }

    void OnDisable()
    {
        _specialAnimationChannelEvent.onEventRaised -= SetSpecial;
    }
    #endregion

    public void Update()
    {
        _playerAnimator.SetFloat("Speed", _chunkMover.GetLevelSpeed() / _speedThreshold);
    }

    public void MoveModel(Vector3 position)
    {
        position.y = math.min(position.y, playerModel.transform.position.y + _maxJumpSpeed);
        playerModel.transform.position = position;
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
