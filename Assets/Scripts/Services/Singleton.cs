/*
A class for to create singletons and make sure that any duplicate instance gets deleted.
*/

using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    private static Singleton<T> _instance;

    public static Singleton<T> Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}
