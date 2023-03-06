using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class GameUtilits
{

    /// <summary>
    /// �����Ƿ�Ϊ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="isShowLog"></param>
    /// <returns></returns>
    public static bool GameIsNull<T>(T t, bool isShowLog = true) where T : class
    {
        if (null == t)
        {
            if (isShowLog)
            {
                Debug.LogErrorFormat($"�����Ͷ���Ϊ�գ����飡����  {typeof(T)}");
            }
            return true;
        }
        return false;
    }





}
