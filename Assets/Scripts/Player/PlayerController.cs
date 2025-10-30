/*
A class to handle player movement.
Uses the new unity input system.
*/

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class PlayerController : Singleton<PlayerController>
{
    public PlayerAnimator Animator;
    private Rigidbody _rb;
    public PlayerPowerUp _playerPowerUp;
    public PlayerStateMachine _playerStateMachine;

    /// <summary>
    /// Reference to the actions our player will take.
    /// </summary>
    #region Actions
    [Header("Movement")]
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _specialAction;
    private InputAction _dashAction;
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
    private float _jumpCooldown = 0.2f;
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
    public override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// We find the reference of all the actions that are possible to take.
    /// </summary>
    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _specialAction = InputSystem.actions.FindAction("Special");
        _dashAction = InputSystem.actions.FindAction("Dash");
    }

    private void Update()
    {
        // Movement
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        _moveValue = new(moveValue.x, 0f, 0f);
        _moveValue.Normalize();

        // Jump
        CheckIfRecoverFromJump();

        if (_jumpAction.IsPressed())
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
            // Note : we can disable gravity for the player object but rather keep it because it looks nice.
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

        // Dash
        if (_dashAction.IsPressed())
        {
            Dash();
        }

        // Special
        if (_specialAction.IsPressed())
        {
            _playerStateMachine.Special(Animator);
        }
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

    #region Jump & Double Jump
    private void CheckIfRecoverFromJump()
    {
        if (!_grounded && IsGrounded())
        {
            RecoverFromJump();
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
        StartCoroutine(JumpPress());
    }

    private IEnumerator JumpPress()
    {
        Animator.SetJump(true);
        _canJump = false;
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }

    private void RecoverFromJump()
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
    private void Dash()
    {
        if (_canDash && _playerPowerUp.CanDash())
        {
            // We check that the player is actually moving and not standing still.
            if (_moveValue * _dashValue != _moveValue)
            {
                transform.Translate(_moveValue * _currentSpeed * _dashValue * Time.deltaTime);
                StartCoroutine(DashPress());
            }
        }
    }

    private IEnumerator DashPress()
    {
        _canDash = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
    #endregion
}
