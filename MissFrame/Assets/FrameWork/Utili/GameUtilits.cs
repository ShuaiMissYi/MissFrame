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
                Debug.LogErrorFormat($"�����Ͷ���Ϊ�գ����飡����  {typeof(T)}");
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
                LogUtilits.LogErrorFormat($"���б����Ϊ��or����Ϊ0�����飡����");
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
                LogUtilits.LogErrorFormat($"���б����Ϊ��or����Ϊ0�����飡����");
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// ������Ϸ����
    /// </summary>
    /// <param name="rootName">���ڵ�����</param>
    /// <param name="relativePath">���·��</param>
    /// <returns></returns>
    public static GameObject FindGameObj(string rootName,string relativePath)
    {
        if (string.IsNullOrEmpty(rootName)||string.IsNullOrEmpty(relativePath))
        {
            LogUtilits.LogErrorFormat("rootName ��  relativePath Ϊ��");
            return null;
        }
        GameObject root = GameObject.Find(rootName);
        if (root.IsNull(true))
        {
            return null;
        }
        GameObject target = root.transform.Find(relativePath).gameObject;
        if (target.IsNull(true))
        {
            return null;
        }
        return target;
    }



}
