/* Class to create collectible objects.*/

using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Collectible : MonoBehaviour, IActivatable
{
    /// <summary>
    /// Channel to declare Collected events
    /// </summary>
    [Header("Collision Events")]
    [SerializeField]
    protected VoidEventChannelSO<int> CollectedEvent;

    [SerializeField]
    private string _canCollectTag = "Player";

    [SerializeField]
    protected int _value;

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

    #region  Monobehaviour methods

    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void Start()
    {
        Activate();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_canCollectTag))
        {
            CollectedEvent?.RaiseEvent(_value);
            Deactivate();
        }
    }
    #endregion
}
