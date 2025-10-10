/*A class to handle player movement.
Uses the new unity input system.*/

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    /// <summary>
    /// Reference to the actions our player will take.
    /// </summary>
    [Header("Movement")]
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _specialAction;

    [Header("Rotation")]
    private Vector3 _rotationDirection = Vector3.zero;
    private float _rotationSpeed = .5f;

    [SerializeField]
    private float _baseSpeed = 10f;

    [SerializeField]
    private float _currentSpeed = 10f;
    private Vector3 _moveValue = Vector3.zero;

    [SerializeField]
    private float _raycastDistance = .01f;

    [SerializeField]
    private float _jumpValue = 1f;

    private int _playerHealth = 1;

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

        //GetComponent<Collider>().isTrigger = true;
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

        if (_jumpAction.IsPressed() && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpValue, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //_rb.AddForce(_moveValue * _currentSpeed, ForceMode.Acceleration);
        transform.Translate(_moveValue * _currentSpeed * Time.deltaTime);
        //transform.Rotate(_rotationDirection * _rotationSpeed);
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * _raycastDistance, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, _raycastDistance);
    }

    void OnColliderEnter(Collider other)
    {
        Debug.Log("A collider has made contact with the DoorObject Collider");
    }

    private void TakeDamage()
    {
        // Check if the player nullifies damage with ability and then resolve effect.
    }

    private void Collect(Collectible collectible)
    {
        // Check if the player nullifies damage with ability and then resolve effect.
    }
}
