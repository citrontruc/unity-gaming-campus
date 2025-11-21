/*
A class to handle player movement.
It relies on player input in order to get the input.
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    #region Components interacting with the playerController
    public PlayerAnimator Animator;
    private Rigidbody _rb;
    public PlayerPowerUp _playerPowerUp;
    public PlayerStateMachine _playerStateMachine;
    #endregion

    /// <summary>
    /// All player inputs are retrieved by the Input component
    /// </summary>
    #region Event Channels
    [Header("Event Channels")]
    [SerializeField]
    private MoveEventChannelSO _moveEventChannel;
    [SerializeField]
    private JumpEventChannelSO _jumpEventChannel;
    [SerializeField]
    private SpecialEventChannelSO _specialEventChannel;
    [SerializeField]
    private DashEventChannelSO _dashEventChannel;
    #endregion

    #region Movement properties
    [SerializeField]
    private float _currentSpeed = 10f;
    private Vector3 _moveValue = Vector3.zero;
    #endregion

    #region Jump properties
    private bool _grounded = true;
    private bool _canDoubleJump = false;
    private bool _canJump = true;

    [SerializeField]
    private float _raycastDistance = .55f;

    [SerializeField]
    private float _jumpValue = 2f;
    private float _jumpCooldown = 0.05f;
    private string _groundTag = "Ground";
    private float _glideValue = 3f;
    private float _glideTimer = 0f;
    private bool _jumpContinuousPress = false;
    #endregion

    #region Dash properties
    private bool _canDash = true;

    [SerializeField]
    private float _dashValue = 20f;
    private float _dashCooldown = 1f;
    #endregion

    #region Monobehaviour Methods
    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// We subscribe to event channels to get user Input.
    /// </summary>
    private void OnEnable()
    {
        _moveEventChannel.onEventRaised += Move;
        _jumpEventChannel.onEventRaised += Jump;
        _dashEventChannel.onEventRaised += Dash;
        _specialEventChannel.onEventRaised += Special;
    }

    private void OnDisable()
    {
        _moveEventChannel.onEventRaised -= Move;
        _jumpEventChannel.onEventRaised -= Jump;
        _dashEventChannel.onEventRaised -= Dash;
        _specialEventChannel.onEventRaised -= Special;
    }

    private void Update()
    {
        CheckIfRecoverFromJump();
    }

    private void FixedUpdate()
    {
        //_rb.AddForce(_moveValue * _currentSpeed, ForceMode.Acceleration);
        transform.Translate(_moveValue * _currentSpeed * Time.deltaTime);
        if (Animator != null)
        {
            Animator.MoveModel(this.transform.position);
        }
    }
    #endregion

    #region Movement
    private void Move(Vector2 moveValue)
    {
        _moveValue = new(moveValue.x, 0f, 0f);
        _moveValue.Normalize();
    }
    #endregion

    #region Jump & Double Jump
    private void Jump(Boolean jumpAction)
    {
        if (jumpAction)
        {
            // When the player presses the button, there is a brief window where he cannot jump.
            if (_canJump)
            {
                switch (_grounded)
                {
                    case false:
                        if (
                            !_jumpContinuousPress
                            && _playerPowerUp.CanDoubleJump()
                            && _canDoubleJump
                        )
                        {
                            Jump();
                            _canDoubleJump = false;
                        }
                        break;
                    case true:
                        if (!_jumpContinuousPress)
                        {
                            Jump();
                        }
                        break;
                }
            }
            // If the player can glide, he will glide by maintaining the jump button.
            // We make him glide by deleting any linearVelocity on the Y axis.
            if (_playerPowerUp.CanGlide())
            {
                _glideTimer += Time.deltaTime;
                if (_glideTimer < _glideValue)
                {
                    _rb.linearVelocity = new Vector3(
                        _rb.linearVelocity.x,
                        0f,
                        _rb.linearVelocity.z
                    );
                }
            }
            _jumpContinuousPress = true;
        }
        else
        {
            _jumpContinuousPress = false;
        }
    }

    private void CheckIfRecoverFromJump()
    {
        if (!_grounded && IsGrounded())
        {
            JumpRecovery();
        }
        _grounded = IsGrounded();
    }

    private bool IsGrounded()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _raycastDistance);
        if (hit.collider is null)
        {
            return false;
        }
        if (hit.collider.CompareTag(_groundTag))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// A method to handle jump, glide and double jump.
    /// </summary>
    private void Jump()
    {
        //_rb.AddForce(Vector3.up * _jumpValue, ForceMode.Impulse);
        transform.Translate(Vector3.up * (_jumpValue + this.transform.position.y));
        StartCoroutine(MakeJumpAction());
    }

    /// <summary>
    /// A method to animate our 3D model
    /// </summary>
    /// <returns></returns>
    private IEnumerator MakeJumpAction()
    {
        Animator.SetJump(true);
        _canJump = false;
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }

    /// <summary>
    /// A method to recover from a jump 
    /// TODO: jump recovery animation
    /// </summary>
    private void JumpRecovery()
    {
        Animator.SetJump(false);
        _canDoubleJump = true;
        _glideTimer = 0f;
        _canJump = true;
    }
    #endregion

    #region Dash actions
    /// <summary>
    /// Logic for the player dash action. A dash is a sudden burst of speed from the player.
    /// </summary>
    private void Dash(Boolean dashAction)
    {
        if (_canDash && _playerPowerUp.CanDash() && dashAction)
        {
            // We check that the player is actually moving and not standing still.
            if (_moveValue * _dashValue != _moveValue)
            {
                transform.Translate(_moveValue * _currentSpeed * _dashValue * Time.deltaTime);
                StartCoroutine(DashRecovery());
            }
        }
    }

    private IEnumerator DashRecovery()
    {
        _canDash = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
    #endregion

    #region Movement
    private void Special(Boolean specialAction)
    {
        if (specialAction)
        {
            _playerStateMachine.Special();
        }
    }
    #endregion
}
