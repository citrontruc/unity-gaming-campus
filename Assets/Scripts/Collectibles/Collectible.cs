/* Class to create collectible objects.*/

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class Collectible : MonoBehaviour
{
    [Header("Collision Events")]
    [SerializeField]
    private ScoreEventChannelSO CollectedEvent;

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
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
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
