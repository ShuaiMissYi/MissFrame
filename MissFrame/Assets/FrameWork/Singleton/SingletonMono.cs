using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̳���Monobehaviour�൥��
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object m_Lock = new object();

    private static T m_Instance;

    public static T GetInstance()
    {
        if (m_Instance == null)
        {
            //������߳�ʱͬʱ����
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

    //��ʼ��
    public void Init()
    {

    }
}


//���൥��
public class Singleton<T> where T : class, new()
{
    private static object m_Lock = new object();

    private static T m_Instance;

    public static T GetInstance()
    {
        if (m_Instance == null)
        {
            //������߳�ʱͬʱ����
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


