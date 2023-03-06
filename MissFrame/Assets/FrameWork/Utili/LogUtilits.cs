using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogUtilits
{


    /// <summary>
    /// 报错打印-对象为空
    /// </summary>
    public static void LogErrorObjIsNull()
    {
        Debug.LogError("对象为空，请检查");
    }


    /// <summary>
    /// 报错打印-空容量
    /// </summary>
    public static void LogErrorCountIsZero() 
    {
        Debug.LogError("容量数量为0，请检查");
    }


}
