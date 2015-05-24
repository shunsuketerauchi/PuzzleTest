using UnityEngine;
using System.Collections;
using System;


public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static readonly string[] tags =
    {
        "SingletonObject",
    };

    protected static T x_instance;

    public static T m_instance
    {
        get
        {
            if (x_instance == null)
            {
                Type type = typeof(T);
                foreach (var it in tags)
                {
                    GameObject[] objs = GameObject.FindGameObjectsWithTag(it);
                    for (int i = 0; i < objs.Length; i++)
                    {
                        x_instance = (T)objs[i].GetComponent(type);
                        if (x_instance != null)
                            return x_instance;
                    }
                }
            }
            return x_instance;
        }

    }

    virtual protected void Awake()
    {
        Check_Instance();
    }

    protected bool Check_Instance()
    {
        if (x_instance == null)
        {
            Type type = typeof(T);
            x_instance = (T)GetComponent(type);
            return true;
        }
        return false;
    }

}
