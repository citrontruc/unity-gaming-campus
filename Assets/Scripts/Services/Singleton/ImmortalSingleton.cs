/*
A special class of singletons for singletons which persist between scenes.
*/

using UnityEngine;

public class ImmortalSingleton<T> : Singleton<T>
    where T : Component
{
    public override void Awake()
    {
        base.Awake();
        if (Singleton<T>.Instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
