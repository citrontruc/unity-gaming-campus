/*
A collectible to give the player another Special power Charge.
*/

using UnityEngine;

public class SpecialCorn : Collectible
{
    [SerializeField]
    private float _rotationSpeed = .5f;

    [SerializeField]
    private SpecialReloadEventChannelSO _specialReloadEventChannel;

    void Awake()
    {
        _value = 5;
    }

    void Start()
    {
        _collectedEvent = _specialReloadEventChannel;
    }

    void Update()
    {
        transform.Rotate(0, _rotationSpeed, 0);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
