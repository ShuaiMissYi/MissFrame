using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//物体显示隐藏单例类
public class ActiveController : SingletonMono<ActiveController>
{
    private ActiveData m_ActiveData;

    public override void Init()
    {
        base.Init();
        AddListener();
    }

    private void AddListener()
    {
        EventDispatcher.GetInstance().Regist(StepShowType.Active,OnActiveCallBack);
        EventDispatcher.GetInstance().Regist(EventType.StopExecuteActive, OnStopExecuteActiveCallBack);
    }

    //查找步骤对象
    private GameObject FindStepActiveGame(ActiveData data)
    {
        if (GameUtilits.GameIsNull(data))
        {
            LogUtilits.LogErrorObjIsNull();
            return null;
        }
        GameObject root = GameObject.Find(m_ActiveData.CfgActiveData.RootName);
        if (GameUtilits.GameIsNull(root))
        {
            LogUtilits.LogErrorObjIsNull();
            return null;
        }
        GameObject target = root.transform.Find(m_ActiveData.CfgActiveData.RelativePath).gameObject;
        if (GameUtilits.GameIsNull(target))
        {
            LogUtilits.LogErrorObjIsNull();
            return null;
        }
        return target;
    }

    private void OnActiveCallBack(params object[] objs)
    {
        if (GameUtilits.GameIsNull(objs) || objs.Length == 0 )
        {
            LogUtilits.LogErrorObjIsNull();
            return;
        }
        m_ActiveData= objs[0]as ActiveData;
        GameObject target = FindStepActiveGame(m_ActiveData);
        target.SetActive(m_ActiveData.CfgActiveData.IsActive);
        EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, m_ActiveData.SubStep);
    }

    private void OnStopExecuteActiveCallBack(params object[] objs)
    {
        if (GameUtilits.GameIsNull(objs) || objs.Length == 0)
        {
            LogUtilits.LogErrorObjIsNull();
            return;
        }
        m_ActiveData = objs[0] as ActiveData;
        GameObject target = FindStepActiveGame(m_ActiveData);
        target.SetActive(!m_ActiveData.CfgActiveData.IsActive);
    }




}
