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

    private Dictionary<int,CacheActiveData> DicCacheActiveData = new Dictionary<int, CacheActiveData>();


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

    private void OnActiveCallBack(params object[] objs)
    {
        if (objs.ArrayIsNull())
        {
            LogUtilits.LogErrorObjIsNull();
            return;
        }
        m_ActiveData= objs[0]as ActiveData;
        //查找
        GameObject target = GameUtilits.FindGameObj(m_ActiveData.CfgActiveData.RootName, m_ActiveData.CfgActiveData.RelativePath);
        target.SetActive(m_ActiveData.CfgActiveData.IsActive);
        //判空
        int subStepId = m_ActiveData.SubStep.CfgSubStepData.Id;
        if (DicCacheActiveData.ContainsKey(subStepId))
        {
            LogUtilits.LogErrorFormat($"包含相同的key值：{subStepId}");
            return;
        }
        //设置
        CacheActiveData cacheData = new CacheActiveData(subStepId);
        cacheData.SetTarget(target);
        DicCacheActiveData.Add(subStepId, cacheData);
        //监听
        EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, m_ActiveData.SubStep);
    }

    private void OnStopExecuteActiveCallBack(params object[] objs)
    {
        if (objs.ArrayIsNull(true))
        {
            return;
        }
        m_ActiveData = objs[0] as ActiveData;
        int subStepId = m_ActiveData.SubStep.CfgSubStepData.Id;
        CacheActiveData caCheData = null;
        if (!DicCacheActiveData.TryGetValue(subStepId,out caCheData))
        {
            LogUtilits.LogErrorFormat($"CacheActiveData数据为空，请检查！！！对应的子步骤id为：{subStepId}");
            return;
        }
        GameObject target = caCheData.Target;
        if (target.IsNull())
        {
            LogUtilits.LogErrorFormat($"对象为空，对应的子步骤id为：{subStepId}");
            return;
        }
        target.SetActive(!m_ActiveData.CfgActiveData.IsActive);
    }




}
