using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//继承自Monobehaviour类单例
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object m_Lock = new object();

    private static T m_Instance;

    public static T GetInstance()
    {
        if (m_Instance == null)
        {
            //避免多线程时同时调用
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = GameObject.FindObjectOfType<T>();
                    if (m_Instance == null)
                    {
                        GameObject go = new GameObject();
                        m_Instance = go.AddComponent<T>();
                        go.name = typeof(T).ToString();
                        DontDestroyOnLoad(go);
                    }
                }
            }
        }
        return m_Instance;
    }

    //初始化
    public void Init()
    {

    }
}


//纯类单例
public class Singleton<T> where T : class, new()
{
    private static object m_Lock = new object();

    private static T m_Instance;

    public static T GetInstance()
    {
        if (m_Instance == null)
        {
            //避免多线程时同时调用
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = new T();
                }
            }
        }
        return m_Instance;
    }
}


