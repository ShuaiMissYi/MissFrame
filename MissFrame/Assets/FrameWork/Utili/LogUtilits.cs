using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogUtilits
{


    /// <summary>
    /// ������ӡ-����Ϊ��
    /// </summary>
    public static void LogErrorObjIsNull()
    {
        Debug.LogError("����Ϊ�գ�����");
    }


    /// <summary>
    /// ������ӡ-������
    /// </summary>
    public static void LogErrorCountIsZero() 
    {
        Debug.LogError("��������Ϊ0������");
    }


}