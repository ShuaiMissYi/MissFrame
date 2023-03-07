using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//������ʾ���ص�����
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
        //����
        GameObject target = GameUtilits.FindGameObj(m_ActiveData.CfgActiveData.RootName, m_ActiveData.CfgActiveData.RelativePath);
        target.SetActive(m_ActiveData.CfgActiveData.IsActive);
        //�п�
        int subStepId = m_ActiveData.SubStep.CfgSubStepData.Id;
        if (DicCacheActiveData.ContainsKey(subStepId))
        {
            LogUtilits.LogErrorFormat($"������ͬ��keyֵ��{subStepId}");
            return;
        }
        //����
        CacheActiveData cacheData = new CacheActiveData(subStepId);
        cacheData.SetTarget(target);
        DicCacheActiveData.Add(subStepId, cacheData);
        //����
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
            LogUtilits.LogErrorFormat($"CacheActiveData����Ϊ�գ����飡������Ӧ���Ӳ���idΪ��{subStepId}");
            return;
        }
        GameObject target = caCheData.Target;
        if (target.IsNull())
        {
            LogUtilits.LogErrorFormat($"����Ϊ�գ���Ӧ���Ӳ���idΪ��{subStepId}");
            return;
        }
        target.SetActive(!m_ActiveData.CfgActiveData.IsActive);
        DicCacheActiveData.Remove(caCheData.SubStepId);
    }




}
