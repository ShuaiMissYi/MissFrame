using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class GameUtilits
{
    public static bool IsNull<T>(this T target, bool isShowLog = false)
    {
        if (null == target)
        {
            if (isShowLog)
            {
                Debug.LogErrorFormat($"该类型对象为空，请检查！！！  {typeof(T)}");
            }
            return true;
        }
        return false;
    }

    public static bool ListIsNull<T>(this List<T> list, bool isShowLog = false)
    {
        if (null == list || list.Count == 0)
        {
            if (isShowLog)
            {
                LogUtilits.LogErrorFormat($"该列表对象为空or长度为0，请检查！！！");
            }
            return true;
        }
        return false;
    }

    public static bool ArrayIsNull<T>(this T[] array, bool isShowLog = false)
    {
        if (null == array || array.Length == 0)
        {
            if (isShowLog)
            {
                LogUtilits.LogErrorFormat($"该列表对象为空or长度为0，请检查！！！");
            }
            return true;
        }
        return false;
    }

}
