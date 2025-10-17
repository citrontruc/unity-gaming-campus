using UnityEngine;

public class SuperCorn : Collectible
{
    [SerializeField]
    private float _rotationSpeed = .5f;

    void Awake()
    {
        _value = 200;
    }

    void Start() { }

    void Update()
    {
        transform.Rotate(0, _rotationSpeed, 0);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
