/*A class to handle player movement.
Uses the new unity input system.*/

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerPowerUp _playerPowerUp => PlayerPowerUp.Instance;

    /// <summary>
    /// Reference to the actions our player will take.
    /// </summary>
    #region Actions 
    [Header("Movement")]
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _specialAction;
    #endregion

    [SerializeField]
    private float _currentSpeed = 10f;
    private Vector3 _moveValue = Vector3.zero;

    #region Jump properties
    private bool _canJump = true;
    [SerializeField]
    private float _raycastDistance = .6f;
    [SerializeField]
    private float _jumpValue = 0.5f;
    private float _jumpCooldown = 0.1f;
    private string _groundTag = "Ground";
    #endregion

    private void Awake()
    {
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
    }

    private void Update()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        _moveValue = new(moveValue.x, 0f, moveValue.y);
        _moveValue.Normalize();

        if (_specialAction.IsPressed())
        {
            Debug.Log("Une action est en cours");
        }

        if (_jumpAction.IsPressed())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        //_rb.AddForce(_moveValue * _currentSpeed, ForceMode.Acceleration);
        transform.Translate(_moveValue * _currentSpeed * Time.deltaTime);
    }

    #region Jump & Double Jump
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
    /// A method to handle jump and double jump.
    /// </summary>
    private void Jump()
    {
        if (_canJump)
        {
            switch (IsGrounded())
            {
                // Double jump
                case false:
                    if (_playerPowerUp.CanDoubleJump())
                    {
                        transform.Translate(Vector3.up * (_jumpValue + this.transform.position.y));
                        _playerPowerUp.ResolveDoubleJump();
                    }
                    break;
                    // Single jump
                case true:
                    //_rb.AddForce(Vector3.up * _jumpValue, ForceMode.Impulse);
                    transform.Translate(Vector3.up * (_jumpValue + this.transform.position.y));
                    _playerPowerUp.ResetDoubleJump();
                    break;
            }
            StartCoroutine(JumpPress());
        }
    }

    private IEnumerator JumpPress()
    {
        _canJump = false;
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }
    #endregion
}
