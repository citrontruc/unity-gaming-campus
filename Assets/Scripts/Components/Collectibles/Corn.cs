using UnityEngine;

public class Corn : Collectible
{
    [SerializeField]
    private float _rotationSpeed = .5f;

    void Awake()
    {
        _value = 10;
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
