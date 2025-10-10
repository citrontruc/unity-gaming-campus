using UnityEngine;

public class Corn : Collectible
{
    [SerializeField]
    private float _rotationSpeed = .5f;

    void Start() { }

    void Update()
    {
        transform.Rotate(0, _rotationSpeed, 0);
    }
}
