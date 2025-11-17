/*
A class to create singletons and make sure that any duplicate instance gets deleted.
There must be only one singleton per scene.
*/

using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Component
{
    #region Singleton access
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindFirstObjectByType(typeof(T));
                if (_instance == null)
                {
                    SetupInstance();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Monobehaviour methods
    public virtual void Awake()
    {
        RemoveDuplicates();
    }

    private static void SetupInstance()
    {
        _instance = (T)FindFirstObjectByType(typeof(T));
        if (_instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            _instance = gameObj.AddComponent<T>();
            //DontDestroyOnLoad(gameObj);
        }
    }
    #endregion

    private void RemoveDuplicates()
    {
        if (_instance == null)
        {
            _instance = this as T;
            //DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
