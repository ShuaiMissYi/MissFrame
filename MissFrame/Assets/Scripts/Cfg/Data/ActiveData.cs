using DG.Tweening;
using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//显示隐藏-数据类
public class ActiveData: ExpressionBase
{

    private CfgActiveData m_CfgActiveData;
    public CfgActiveData CfgActiveData => m_CfgActiveData;

    public ActiveData(int id):base(id)
    {
        
    }

    //使用该类时，重置一次，以免场景对象丢失
    public override void InitCfgData()
    {
        InitCfgData(m_ShowId);
    }

    private void InitCfgData(int id)
    {
        m_CfgActiveData = CfgUtility.GetInstance().CfgTab.TbActive.Get(id);
        if (null == CfgActiveData)
        {
            Debug.LogErrorFormat($"该id：{id}对应的配置为空！！！");
            return;
        }
    }


    public override void Run(SubStepData data)
    {
        base.Run(data);
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.Active, this);
    }
    public override void Reset()
    {
        base.Reset();
        EventDispatcher.GetInstance().DispatchEvent(EventType.StopExecuteActive, this);
    }


}
