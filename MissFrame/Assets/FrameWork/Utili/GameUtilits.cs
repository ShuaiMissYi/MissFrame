using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class GameUtilits
{

    /// <summary>
    /// 对象是否为空
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
                Debug.LogErrorFormat($"该类型对象为空，请检查！！！  {typeof(T)}");
            }
            return true;
        }
        return false;
    }





}
