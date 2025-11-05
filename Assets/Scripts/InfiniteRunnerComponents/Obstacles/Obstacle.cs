/*
A class to implement obstacles the player musst avoid.
*/

using UnityEngine;

public class Obstacle : MonoBehaviour, IActivatable
{
    #region Collision properties
    [Header("Collision Events")]
    [SerializeField]
    private ObstacleCollisionEventChannelSO CollisionEvent;

    [SerializeField]
    private string _playerTag = "Player";
    #endregion

    #region Obstacle properties
    public enum Resistance
    {
        small = 0,
        large = 1,
        unbreakable = 2,
    }

    [SerializeField]
    private Resistance _resistance = Resistance.small;
    #endregion

    #region Setters and Getters
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Monobehaviour methods
    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            CollisionEvent?.RaiseEvent(_resistance);
        }
    }
    #endregion
}
