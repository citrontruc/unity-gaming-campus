/*A class to handle player movement.
Uses the new unity input system.*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Reference to the actions our player will take.
    /// </summary>
    [Header("Movement")]
    private InputAction _moveAction;
    private InputAction _specialAction;

    [SerializeField]
    private float _baseSpeed = 10f;

    [SerializeField]
    private float _currentSpeed = 10f;

    private int _playerHealth = 1;

    private void Awake() { }

    /// <summary>
    /// We find the reference of all the actions that are possible to take.
    /// </summary>
    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _specialAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        Vector3 moveTranslation = new(moveValue.x, 0f, moveValue.y);
        transform.Translate(moveTranslation * _currentSpeed * Time.deltaTime);

        if (_specialAction.IsPressed())
        {
            Debug.Log("Une action est en cours");
        }
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
