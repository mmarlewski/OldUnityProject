using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T I { get { return instance; } }

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this as T;
        }
    }
}
