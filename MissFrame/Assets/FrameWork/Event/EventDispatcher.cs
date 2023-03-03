/*******************************************************************
Description:  事件管理
********************************************************************/


using UnityEngine;
using System.Collections.Generic;
using MissFrame.Trigger;

public delegate void MyEventHandler(params object[] objs);

public class EventDispatcher:Singleton<EventDispatcher>{

    private Dictionary<EventType, MyEventHandler> listenerCommonDic = new Dictionary<EventType, MyEventHandler>();
    private Dictionary<StepShowType, MyEventHandler> listenerStepDic = new Dictionary<StepShowType, MyEventHandler>();
    private readonly string szErrorMessage = "DispatchEvent Error, Event:{0}, Error:{1}, {2}";


    #region 注册事件
    /// <summary>
    /// 注册普通监听事件
    /// </summary>
    /// <param name="evt"></param>
    /// <param name="handler"></param>
    public void Regist(EventType evt, MyEventHandler handler)
    {
        if (null == handler )
            return;
        
        if (listenerCommonDic.ContainsKey(evt))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]+=..
            listenerCommonDic[evt] += handler;
        }
        else
        {
            listenerCommonDic.Add(evt, handler);
        }
    }
    /// <summary>
    /// 注册步骤式事件监听
    /// </summary>
    /// <param name="type"></param>
    /// <param name="handler"></param>
    public void Regist(StepShowType type,MyEventHandler handler)
    {
        if (null == handler)
            return;
        if (listenerStepDic.ContainsKey(type))
        {
            listenerStepDic[type] += handler;
        }
        else
        {
            listenerStepDic.Add(type, handler);
        }
    }
    #endregion

    #region 注销事件
    /// <summary>
    /// 注销事件
    /// </summary>
    /// <param name="evt">事件名</param>
    /// <param name="handler">响应函数</param>
    public void UnRegist(EventType evt, MyEventHandler handler)
    {
        if (null == handler)
            return;
        if (listenerCommonDic.ContainsKey(evt))
        {
            listenerCommonDic[evt] -= handler;
            if (null == listenerCommonDic[evt])
            {
                listenerCommonDic.Remove(evt);
            }
        }
    }
    /// <summary>
    /// 注销事件
    /// </summary>
    /// <param name="evt">事件名</param>
    /// <param name="handler">响应函数</param>
    public void UnRegist(StepShowType type, MyEventHandler handler)
    {
        if (null == handler)
            return;
        if (listenerStepDic.ContainsKey(type))
        {
            listenerStepDic[type] -= handler;
            if (null == listenerStepDic[type])
            {
                listenerStepDic.Remove(type);
            }
        }
    }
    #endregion


    #region 抛出事件
    /// <summary>
    /// 抛出事件
    /// </summary>
    /// <param name="evt">事件名</param>
    /// <param name="objs">参数</param>
    public void DispatchEvent(EventType evt, params object[] objs)
    {
        try
        {
            if (listenerCommonDic.ContainsKey(evt))
            {
                MyEventHandler handler = listenerCommonDic[evt];
                if (null != handler)
                    handler(objs);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogErrorFormat(szErrorMessage, evt, ex.Message, ex.StackTrace);
        }
    }
    /// <summary>
    /// 抛出事件
    /// </summary>
    /// <param name="evt">事件名</param>
    /// <param name="objs">参数</param>
    public void DispatchEvent(StepShowType evt, params object[] objs)
    {
        try
        {
            if (listenerStepDic.ContainsKey(evt))
            {
                MyEventHandler handler = listenerStepDic[evt];
                if (null != handler)
                    handler(objs);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogErrorFormat(szErrorMessage, evt, ex.Message, ex.StackTrace);
        }
    }
    #endregion


    #region 清理监听key
    public void ClearEventsByKey(EventType key)
    {
        if (listenerCommonDic.ContainsKey(key))
        {
            listenerCommonDic.Remove(key);
        }
    }
    public void ClearEventsByKey(StepShowType key)
    {
        if (listenerStepDic.ContainsKey(key))
        {
            listenerStepDic.Remove(key);
        }
    }
    #endregion


}

