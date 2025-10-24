/*
A class to create singletons and make sure that any duplicate instance gets deleted.
There must be only one singleton per scene.
*/

using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Component
{
    #region Singleton access
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindFirstObjectByType(typeof(T));
                if (instance == null)
                {
                    SetupInstance();
                }
            }
            return instance;
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
        instance = (T)FindFirstObjectByType(typeof(T));
        if (instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            instance = gameObj.AddComponent<T>();
            //DontDestroyOnLoad(gameObj);
        }
    }
    #endregion

    private void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = this as T;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
