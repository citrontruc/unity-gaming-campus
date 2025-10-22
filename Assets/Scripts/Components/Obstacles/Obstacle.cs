using UnityEngine;

public class Obstacle : MonoBehaviour, IActivatable
{
    [Header("Collision Events")]
    [SerializeField]
    private ObstacleCollisionEventChannelSO CollisionEvent;

    public enum Resistance
    {
        small = 0,
        large = 1,
        unbreakable = 2,
    }

    [SerializeField]
    private Resistance _resistance = Resistance.small;

    [SerializeField]
    private string _playerTag = "Player";

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
}
